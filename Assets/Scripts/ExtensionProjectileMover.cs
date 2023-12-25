using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionProjectileMover : ProjectileMover
{   
    float attackRange;
    // Start is called before the first frame update
    protected override void Start()
    {   
        base.Start();

        var hitPs = hit.GetComponent<ParticleSystem>();
        float particleRadius = hitPs.main.startSize.constantMax / 2f;
        float particleVolume = 4f / 3f * Mathf.PI * Mathf.Pow(particleRadius, 3);
            // 부피에서 반지름을 계산
        attackRange = Mathf.Pow(3f * particleVolume / (4f * Mathf.PI), 1f / 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter(Collision collision)
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
             // Plus Code;
            // float particleRadius = hitPs.main.startSize.constantMax / 2f;
            // float particleVolume = 4f / 3f * Mathf.PI * Mathf.Pow(particleRadius, 3);
            // // 부피에서 반지름을 계산
            // float attackRange = Mathf.Pow(3f * particleVolume / (4f * Mathf.PI), 1f / 3f);
            // 계산된 반지름이 실제 공격 범위
            Collider[] colliders = Physics.OverlapSphere(pos, attackRange, collisionLayer);
            foreach(var collider in colliders) {
                var monster = collider.GetComponent<Monster>();
                if(monster != null) 
                    monster.OnDamage(damage);
            }
        }
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

    public override void SetUp(Monster targetMonster, double damage)
    {
        base.SetUp(targetMonster, damage);
        if(targetMonster == null) {
            Debug.LogError("Target Monster is Null");
            return;
        }
    }

}
