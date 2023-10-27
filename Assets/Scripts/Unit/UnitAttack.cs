using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{   

    [SerializeField]
    private LayerMask monsterLayerMask;
    public Transform projectilePrefab;
    public Transform handPosition;
    
    private UnitMovement unitMovement;
    private UnitStats unitStats;

    private Animator anim;
    private LineRenderer line;

   // private Transform targetTransform;
    private Monster targetMonster;
    private float attackTimer;
    private bool isAttacking = false;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        unitMovement = this.GetComponent<UnitMovement>();
        unitStats = this.GetComponent<UnitStats>();
       // line = this.GetComponent<LineRenderer>();
    }

    void Start() {
        // line.startColor = Color.red;
        // line.endColor = Color.red;

        // line.startWidth = 0.1f;
        // line.endWidth = 0.1f;
        
        attackTimer = unitStats.AttackSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        // 타이머 업데이트
        // Debug.Log(attackTimer);
        // 공격 간격을 초과한 경우 공격시행
        if(!isAttacking) {
            if(attackTimer < unitStats.AttackSpeed) {
                attackTimer += Time.deltaTime;
            } else {
                isAttacking = true;
            }
        }

        if(!unitMovement.isMoving && isAttacking && targetMonster == null) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, unitStats.AttackRange, monsterLayerMask);
            if(hitColliders.Length > 0) {
                Collider target = hitColliders[0];
                targetMonster = target.GetComponent<Monster>();
                this.transform.LookAt(targetMonster.transform);
                anim.SetTrigger("Attack");
                // anim.SetFloat("attackSpeed", unitStats.AttackSpeed);
            } 
        }
    

    
            
    
        
        // // 타겟 몬스터가 존재
        // if (targetMonster != null)
        // {   
        //     // 타겟 몬스터와의 거리가 공격 사정거리보다 클 때
        //     if (Vector3.Distance(transform.position, targetMonster.transform.position) > attackRange)
        //     {
        //         targetMonster = null;
        //         line.enabled = false;
        //     }
        //     // 공격 사정거리 범위 내 몬스터가 존재할 때
        //     else
        //     {   
        //         this.transform.LookAt(targetMonster.transform);
        //         anim.SetTrigger("Attack");
        //     }
        // }
    }

    // public void SetUp(float newDamage, float newAttackSpeed, float newAttackRange) {
    //     this.damage = newDamage;
    //     this.attackSpeed = newAttackSpeed;
    //     this.attackRange = newAttackRange;
    // }

    public void StartAttack()
    {   
        if(targetMonster != null) {

        Vector3 targetDirection = (targetMonster.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);  

        Transform projectileInstance = Instantiate(projectilePrefab, handPosition.position, targetRotation);

        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        projectile.SetUp(targetMonster, unitStats.Damage);

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
