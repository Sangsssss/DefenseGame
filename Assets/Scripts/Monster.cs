using System;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    
    //private Rigidbody rigidbody;
    private Animator anim;
    private NavMeshAgent agent;

    private Transform[] wayPoints;
    private int currentWayPoint = 0;

    // Delete After
    private float health;
    public int gold;
    
    private int roopCount;
    public bool isDied = false;
    public int limitedRoop;

    public event Action OnDeath;
    public event Action OnAttack;

    private void Awake() {
        //rigidbody = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        roopCount = 0;
    }

    void Start()
    {   
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
        if(isDied && agent.remainingDistance <= agent.stoppingDistance) {
            MoveToNextWayPoint();
        }
    }

    public void SetUp(float health, int gold, Transform[] wayPoints) {
        this.wayPoints = wayPoints; 
        this.health = health;
        this.gold = gold;
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
            health -= damage;
            if(health <= 0 && !isDied) {
                Die();
            }
    }

        private void Die() {
            OnDeath?.Invoke();
            isDied = true;
        }

        private void Attack() {
            OnAttack?.Invoke();
            isDied = true;
        }
}