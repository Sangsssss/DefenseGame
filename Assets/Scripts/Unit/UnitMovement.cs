
using System;
using System.Collections;
using System.ComponentModel;
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
    private Animator anim;
    private Rigidbody rigidbody;
    private LineRenderer line;
    
    Coroutine draw;

    private RigidbodyConstraints originalContraints;
    public bool isMoving;
    public Action OnSell;
    

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
         if(!agent.isStopped && agent.remainingDistance <= 0.2f) {
            // Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
            // if(colliders.Length == 0) {
                Freeze();
            // } else {
            //     foreach(Collider collider in colliders) {
            //         if(collider.gameObject != gameObject && collider == collider.CompareTag("Player")) {
            //             anim.SetFloat("Move", 0.0f);
            //             break;
            //         }
            // }
            
            // }
         
            
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
        agent.isStopped = !isMoving;
        // if(draw != null) StopCoroutine(draw);
        //         draw = StartCoroutine(DrawPath());
        
    }

    public void Stop(bool stop) {

    }

    public void Sell() {
        OnSell?.Invoke();
    }


}
