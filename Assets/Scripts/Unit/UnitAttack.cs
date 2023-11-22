using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{   

    [SerializeField]
    private LayerMask monsterLayerMask;
    public GameObject projectilePrefab;
    public Transform muzzlePoint;
    
    private UnitMovement unitMovement;
    private UnitStats unitStats;

    private Animator anim;
    private LineRenderer line;

   // private Transform targetTransform;
    private Collider target;
    private float shortDis;

    private Monster targetMonster;
    private float attackTimer;
    private bool isAttacking = false;
    [SerializeField] private AudioClip attackSound;

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
                anim.SetTrigger("Attack");
                
                // anim.SetFloat("attackSpeed", unitStats.AttackSpeed);
            } 
        }
    
    }

    // public void SetUp(float newDamage, float newAttackSpeed, float newAttackRange) {
    //     this.damage = newDamage;
    //     this.attackSpeed = newAttackSpeed;
    //     this.attackRange = newAttackRange;
    // }

    public void StartAttack()
    {   
        if(targetMonster != null) {
        GameManager.instance.PlayUnitAttackSound(attackSound);

        // Vector3 targetDirection = (targetMonster.transform.position - transform.position).normalized;
        // Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);  

        GameObject projectileInstacne = Instantiate(projectilePrefab, muzzlePoint.position, transform.rotation);

        HS_ProjectileMover projectile = projectileInstacne.GetComponent<HS_ProjectileMover>();
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
