
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Numerics;
using Unity.Android.Gradle;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class UnitMovement : MonoBehaviour
{
    // Start is called before the first frame updat
    private NavMeshAgent agent;
    [SerializeField] private LayerMask obstacleLayer;
    private Animator anim;
    private Rigidbody rigidbody;
    private LineRenderer line;
    
    Coroutine draw;

    private RigidbodyConstraints originalContraints;
    public bool isMoving;
    public Action OnSell;


    public float sphereRadius = 0.3f;
    public Color gizmosColor = Color.red;
    
      private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }

    void Awake() {
        rigidbody = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        line = this.GetComponent<LineRenderer>();
        //spot = Instantiate(spotPrefab, transform.position, UnityEngine.Quaternion.identity);
    }

    void Start()
    {   

        // originalContraints = rigidbody.constraints;
        // line.startWidth = 0.3f;
        // line.endWidth = 0.3f;
        // line.enabled = false;
        
    }

    void Update() {
       // agent가 도착하고자 하는 위치에 무언가 있다면 멈춰라
        if (!agent.isStopped && agent.remainingDistance <= 0.0f) {
            Freeze();
            return;
        }

        // // agent가 거의 멈춰있고 남은 거리가 일정 이내라면 멈춰라
        // if (!agent.isStopped && agent.velocity.magnitude <= 0.1f && agent.remainingDistance <= 3.0f) {
        //     Freeze();
        //     return;
        // }

        // agent가 거의 멈춰있고 남은 거리가 매우 가까우며 장애물이 있다면 멈춰라
        if (!agent.isStopped && agent.remainingDistance <= 0.5f) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f, obstacleLayer);
            if (colliders.Length > 0) {
                Debug.Log(colliders.Length + " , No more Move");
                Freeze();
            }
        }
    }
        

    public void Freeze() {
        // if(rigidbody.constraints == originalContraints) {
        //     rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        // } else {
        //     rigidbody.constraints = originalContraints;
        // }
        agent.isStopped = true;
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        //line.enabled = false;
        // if(draw != null) StopCoroutine(draw);
        isMoving = false;
        agent.avoidancePriority = 50;
    }

    

//    IEnumerator DrawPath() {
//         line.enabled = true;
//         yield return null;
//         while(true) {
//             int cnt = agent.path.corners.Length;
//             line.positionCount = cnt;
            
//             for(int i = 0; i < cnt; i++) {
//                 line.SetPosition(i, agent.path.corners[i]);
//         }
//         yield return null;
//     }
// }

    // public void SelectUnit() {
    //     Debug.Log("Select Unit");
    //     selectCircle.SetActive(true);
    //     UIManager.instance.ShowUnitStatus(transform.position.x, transform.position.z);
    // }

    // public void DeselectUnit() {
    //     selectCircle.SetActive(false);
    // }

    public void Move(UnityEngine.Vector3 Destination) {
        UnityEngine.Vector3 direction = (Destination - transform.position).normalized;
            // x와 z축의 방향에 따라 애니메이션을 결정합니다.
        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.z);

        // transform.LookAt(Destination);
        agent.SetDestination(Destination);
        // anim.SetFloat("Move", 1.0f);
        isMoving = true;
        agent.avoidancePriority = 51;
        
        agent.isStopped = !isMoving;

      
        // if(draw != null) StopCoroutine(draw);
        //         draw = StartCoroutine(DrawPath());
        
    }

    public void NotReachDestination(NavMeshPath path) {
        Debug.Log("PARTIAL PATH!");
             // 최종 목적지까지의 경로가 부분적으로 생성되었을 때의 처리
        UnityEngine.Vector3 lastCorner = path.corners[path.corners.Length - 1];

            // 이동 중인지 확인
        bool isMoving = agent.velocity.magnitude > 0.1f;

            // lastCorner와의 거리 체크
        float distanceToLastCorner = UnityEngine.Vector3.Distance(transform.position, lastCorner);

            // 일정 거리 이내에 있으면 이동을 멈춤
            if (isMoving && distanceToLastCorner < 0.1f)
            {
                Freeze();
            }
                
    }

    public void Sell() {
        OnSell?.Invoke();
    }


}
