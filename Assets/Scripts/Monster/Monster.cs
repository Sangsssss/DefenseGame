using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour, IDamageable
{   
    // Start is called before the first frame update
    [SerializeField] private AudioClip monsterDeathSound;
    private string monsterName;
    private int wave;
    public int Wave {get {return wave;}}
    private double hp;
    public double HP {get {return hp;}}
    private double currentHP;
    public double CurrentHP {get {return currentHP;}}
    private int damage;
    private int gold;
    private Transform[] wayPoints;

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


    // // HP
    // [SerializeField] Slider HPBar;
    // [SerializeField] private Vector3 hpBarOffset; // HP 바의 위치 오프셋
    // [SerializeField] private Quaternion hpBarRotation;


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
         }

    }

    // void LateUpdate()
    // {
    //     HPBar.transform.rotation = hpBarRotation;
    // }

    void Update()
    {   
        // HPBar.transform.position = transform.position + hpBarOffset;
        // UpdateHP();
        if(roopCount == limitedRoop) {
            // 몬스터 => Player Attack 시행
            Attack();  
        }
        if(!isDied && agent.remainingDistance <= agent.stoppingDistance) {
            MoveToNextWayPoint();
        }
    }

    
    // private void UpdateHP() {
    //     HPBar.value = (float)(currentHP / hp);
    // }


    public void SetUpMonster(MonsterData monsterData) {
        this.monsterName = monsterData.monsterName;
        this.wave = monsterData.wave;
        this.hp = monsterData.health;
        this.currentHP = hp;
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

        public void OnDamage(double damage)
        {
            this.currentHP -= damage;
            if(this.currentHP <= 0 && !isDied) {
                Die();
            }
        }

        private void Die() {
            OnDeath?.Invoke();
            isDied = true;
            GameManager.instance.PlayMonsterDeathSound(monsterDeathSound);
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
