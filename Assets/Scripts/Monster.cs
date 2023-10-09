using System;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    private string monsterName;
    private int wave;
    public int Wave {get {return wave;}}
    private float health;
    private int damage;
    private int gold;
    private Transform[] wayPoints;
    public MonsterData monsterData;


    //private Rigidbody rigidbody;
    private Animator anim;
    private NavMeshAgent agent;

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


    public void SetUpMonster(MonsterData monsterData) {
        this.monsterName = monsterData.monsterName;
        this.wave = monsterData.wave;
        this.health = monsterData.health;
        this.damage = monsterData.damage;
        this.gold = monsterData.gold;
        this.wayPoints = monsterData.monsterCommonData.WayPoints;
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
            this.health -= damage;
            if(this.health <= 0 && !isDied) {
                Die();
            }
        }

        private void Die() {
            OnDeath?.Invoke();
            isDied = true;
            if(last == true) {
                GameManager.instance.GainGold(this.gold);
            }
        }

        private void Attack() {
            OnAttack?.Invoke();
            GameManager.instance.LoseLife(this.damage);
            isDied = true;
        }

        public void IsLast() {
            last = true;
        }
}
