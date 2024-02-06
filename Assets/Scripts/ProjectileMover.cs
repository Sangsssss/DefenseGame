using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    public Rigidbody rb;
    public GameObject[] Detached;
    public double damage;
    public Monster targetMonster;
    [SerializeField] public LayerMask collisionLayer;  

    protected virtual void Start()
    {   
        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            //Instantiate flash effect on projectile position
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            
            //Destroy flash effect depending on particle Duration time
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject,5);
	}

    // real-Time Update를 위함
    public void FixedUpdate ()
    {
		if (speed != 0)
        {   
            if(targetMonster != null) {
            //Vector3 direction = (targetPosition - transform.position).normalized;
            rb.velocity = transform.forward * speed;
            Vector3 targetPos = targetMonster.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetPos);
            //transform.position += transform.forward * (speed * Time.deltaTime);         
            }
        }
	}

    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    protected virtual void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Unit"))
        {
            return;
        }
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;
               
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        // Quaternion rot = Quaternion.FromToRotation(Vector3.up, targetPosition);
        Vector3 pos = contact.point + contact.normal * hitOffset;
        // Vector3 pos = targetPosition - transform.position;

        //Spawn hit effect on collision
        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { hitInstance.transform.LookAt( contact.point + contact.normal); }

            //Destroy hit effects depending on particle Duration time
            var hitPs = hitInstance.GetComponent<ParticleSystem>(); 

           

            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
            //  // Plus Code;
            // float particleRadius = hitPs.main.startSize.constantMax / 2f;
            // float particleVolume = 4f / 3f * Mathf.PI * Mathf.Pow(particleRadius, 3);
            // // 부피에서 반지름을 계산
            // float attackRange = Mathf.Pow(3f * particleVolume / (4f * Mathf.PI), 1f / 3f);
            // // 계산된 반지름이 실제 공격 범위
            // Collider[] colliders = Physics.OverlapSphere(pos, attackRange, collisionLayer);
            // foreach(var collider in colliders) {
            //     var monster = collider.GetComponent<Monster>();
            //     if(monster != null) 
            //         monster.OnDamage(damage);
            // }
              targetMonster.OnDamage(damage);
        }
        //   //Destroy projectile on collision\
      
        //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
                Destroy(detachedPrefab, 1);
            }
        }
        Destroy(gameObject);
    }

    
    public virtual void SetUp(Monster targetMonster, double damage) {
        this.targetMonster = targetMonster;
        this.damage = damage;
        if(targetMonster == null) {
            Debug.LogError("Target Monster is Null");
        } else
            Debug.Log("Target Monster : " + this.targetMonster.name);
    }
}
