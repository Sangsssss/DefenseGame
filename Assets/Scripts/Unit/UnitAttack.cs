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
    private float attackTimer;
    private bool isAttacking = false;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private float animAttackSpeed = 1.0f;

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
        attackTimer = 1/unitStats.AttackSpeed;
        Debug.Log("AttackTimer :"  + attackTimer);
    }

    // Update is called once per frame
    private void Update()
    {   
        // 타이머 업데이트
        // Debug.Log(attackTimer);
        // 공격 간격을 초과한 경우 공격시행
        if(!isAttacking) {
            if(attackTimer < 1/unitStats.AttackSpeed) {
                attackTimer += Time.deltaTime;
            } else {
                isAttacking = true;
            }
        }

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
                this.transform.LookAt(targetMonster.transform);
                
                // 여기서 유닛의 공격 애니메이션 속도를 조절해야할듯??
                // animAttackSpeed = AdjustAttackSpeed(unitStats.AttackSpeed);
                // float animAttackSpeed = 1.0f;
                anim.SetFloat("Attack_Speed", animAttackSpeed);
                anim.SetTrigger("Attack");
                // anim.SetFloat("attackSpeed", unitStats.AttackSpeed);
            } 
        }
    
    }

    private float AdjustAttackSpeed(float attackSpeed) {
        return ANIMATION_ATTACK_SPPED + attackSpeed; 
    }

    // public void SetUp(float newDamage, float newAttackSpeed, float newAttackRange) {
    //     this.damage = newDamage;
    //     this.attackSpeed = newAttackSpeed;
    //     this.attackRange = newAttackRange;
    // }

    public void StartAttack()
    {   
        if(targetMonster != null) {
            // weapon에서 공격 수행하도록
            weapon.Shooting(targetMonster, unitStats.Damage);

            isAttacking = false;
            attackTimer = 0.0f;
            targetMonster = null;
        
        //line.enabled = true;

        // Vector3 attackDirection = (targetMonster.transform.position - this.transform.position).normalized;
        // Ray ray = new Ray(transform.position, attackDirection);
        // RaycastHit hit;

        // if (Physics.Raycast(ray, out hit, attackRange))
        // {
        //     Debug.Log("레이저 발사");
        //     targetMonster?.onDamage(damage,hit);
        //     // if (hit.collider.gameObject == targetMonster)
        //     // {         
        //     //     Monster target = hit.collider.GetComponent<Monster>();
        //     //     target?.onDamage(damage, hit);
        //     //     Debug.Log("공격 성공" + target.name);
        //     // } 
        // } 
        }
    }
}
