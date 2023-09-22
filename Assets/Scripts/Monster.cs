using System;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    [SerializeField]
    private MonsterData monsterData;
    public MonsterData MonsterData { set {monsterData = value;} }


    //private Rigidbody rigidbody;
    private Animator anim;
    private NavMeshAgent agent;

    private Transform[] wayPoints;
    private int currentWayPoint = 0;

    // Delete After
    
    private int roopCount;
    public bool isDied = false;
    private int limitedRoop = 1;

    public event Action OnDeath;
    public event Action OnAttack;

    private bool last;


    private void Awake() {
        //rigidbody = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        roopCount = 0;
        last = false;
    }

    void Start()
    {  
        wayPoints = monsterData.CommonData.WayPoints;
         if(wayPoints.Length == 0) {
            Debug.LogWarning("No waypoint");
         } else {
            MoveToNextWayPoint();
            Debug.Log(anim.GetFloat("Speed"));
         }

    }

    void Update()
    {   
        if(roopCount == limitedRoop) {
            // 몬스터 => Player Attack 시행
            Attack();  
        }
        if(!isDied && agent.remainingDistance <= agent.stoppingDistance) {
            MoveToNextWayPoint();
        }
    }

    
    public void WatchMonsterInfo()
    {   
        Debug.Log("몬스터 이름 :: " + monsterData.MonsterName);
        Debug.Log("몬스터 wave :: " + monsterData.Wave);
        Debug.Log("몬스터 체력 :: " + monsterData.Health);
        Debug.Log("몬스터 획득골드 :: " + monsterData.Gold);
    }
    

    private void MoveToNextWayPoint() {
        agent.isStopped = false;
        agent.updateRotation = true;
        currentWayPoint = (currentWayPoint) % wayPoints.Length;
        if(currentWayPoint == 3) {
            roopCount++;
        }
        anim.SetFloat("Speed", 1f);
        agent.SetDestination(wayPoints[currentWayPoint].position);
        currentWayPoint++;
    }

        public void onDamage(float damage, RaycastHit hit)
        {
            monsterData.Health -= damage;
            if(monsterData.Health <= 0 && !isDied) {
                Die();
            }
        }

        private void Die() {
            OnDeath?.Invoke();
            isDied = true;
            if(last == true) {
                GameManager.instance.GainGold(monsterData.Gold);
            }
        }

        private void Attack() {
            OnAttack?.Invoke();
            GameManager.instance.LoseLife(monsterData.Damage);
            isDied = true;
        }

        public void IsLast() {
            last = true;
        }
}
