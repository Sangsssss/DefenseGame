using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{   

    [SerializeField]private LayerMask monsterLayerMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Weapon weapon;
    
    private UnitMovement unitMovement;
    private UnitStats unitStats;

    private Animator anim;

   // private Transform targetTransform;
    private Collider target;
    private float shortDis;

    private Monster targetMonster;
    [SerializeField] private float attackInterval; // 다음 공격 간격(초)
    private float lastAttackTime; // 이전 공격 시간
    private bool isAttacking; // 공격 중인지?
    [SerializeField] private AudioClip attackSound;
    private float animAttackSpeed;
    private static float ANIMATION_ATTACK_SPPED = 1.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        unitMovement = this.GetComponent<UnitMovement>();
        unitStats = this.GetComponent<UnitStats>();
       // line = this.GetComponent<LineRenderer>();
    }

    void Start() {
        weapon.SetUp(projectilePrefab);
        isAttacking = false;
        // attackTimer = 1/unitStats.AttackSpeed;
        lastAttackTime = Time.time; // 초기화
        animAttackSpeed = AdjustAttackSpeed(unitStats.AttackSpeed);
        anim.SetFloat("Attack_Speed", animAttackSpeed);
    }

    // Update is called once per frame
    private void Update()
    {   
        float timeSinceLastAttack = Time.time - lastAttackTime;
        if (timeSinceLastAttack >= 1/unitStats.AttackSpeed && !isAttacking) {
            Debug.Log("OnAttack! " + timeSinceLastAttack);
            isAttacking = true;
            lastAttackTime = Time.time;
        }
        // 타이머 업데이트
        // Debug.Log(attackTimer);
        // 공격 간격을 초과한 경우 공격시행
        // if(!isAttacking) {
        //     if(attackTimer < 1/unitStats.AttackSpeed) {
        //         attackTimer += Time.deltaTime;
        //     } else {
        //         isAttacking = true;
        //         Debug.Log(Time.time);
        //     }
        // }

        if(!unitMovement.isMoving && isAttacking && targetMonster == null) {
            // 현재 유닛의 위치에서 스피어 반경 attackRange에 있는 몬스터를 감지해서 collider 배열에 저장
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, unitStats.AttackRange, monsterLayerMask);
            // 사정거리 안에 타겟이 존재할 시
            
            if(hitColliders.Length > 0) {
                target = hitColliders[0];
                shortDis = UnityEngine.Vector3.Distance(transform.position, target.transform.position);
                foreach(Collider hitCollider in hitColliders) {
                    float distance = UnityEngine.Vector3.Distance(transform.position, hitCollider.transform.position);
                    if(distance < shortDis) {
                        target = hitCollider;
                        shortDis = distance;
                    }
                }
                targetMonster = target.GetComponent<Monster>();
                StartAttack(targetMonster);
                isAttacking = false;  
                targetMonster = null;
            } 
        }
    
    }

    private float AdjustAttackSpeed(float attackSpeed) {
        return ANIMATION_ATTACK_SPPED * attackSpeed;
    }

    // public void SetUp(float newDamage, float newAttackSpeed, float newAttackRange) {
    //     this.damage = newDamage;
    //     this.attackSpeed = newAttackSpeed;
    //     this.attackRange = newAttackRange;
    // }

    // 투사체를 발사함
    public void StartAttack(Monster targetMonster)
    {   
        Debug.Log("타겟의 이름은 : " + targetMonster.MonsterName);
        this.transform.LookAt(targetMonster.transform);        
        anim.SetTrigger("Attack");

        if(targetMonster != null) {
            // weapon에서 공격 수행하도록
            weapon.Shooting(targetMonster, unitStats.Damage);
        }
        
    }
}
