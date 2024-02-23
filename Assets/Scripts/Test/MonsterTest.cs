using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterTest : MonoBehaviour, IDamageable
{   
    // Start is called before the first frame update
    [SerializeField] private AudioClip monsterDeathSound;
    [SerializeField] private string monsterName;
    public string MonsterName {get {return monsterName;}}
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

    public bool isDied = false;
 
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
        last = false;
    }

    void Start()
    {  

    }

    void Update()
    {   
    }

    
    // private void UpdateHP() {
    //     HPBar.value = (float)(currentHP / hp);
    // }

    public void SetUpMonster(MonsterData monsterData) {
        this.monsterName = monsterData.monsterName + UnityEngine.Random.Range(0, 1000);
        this.wave = monsterData.wave;
        this.hp = monsterData.health;
        this.currentHP = hp;
        this.damage = monsterData.damage;
        this.gold = monsterData.gold;
        this.wayPoints = monsterData.monsterCommonData.WayPoints;
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
    }
}
