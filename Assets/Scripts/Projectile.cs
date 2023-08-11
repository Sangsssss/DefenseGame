using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 50f;
    public float damage;
    private RaycastHit hit;

    private Monster targetMonster;
    private Vector3 targetPosition;

    public ParticleSystem particles;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        if (targetMonster != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            transform.LookAt(targetMonster.transform);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                if (targetMonster != null)
                {
                    targetMonster.onDamage(damage, hit);
                    Destroy(gameObject);
                }
            }    
        }


    }

    public void SetUp(Monster targetMonster, float damage) {
        this.targetMonster = targetMonster;
        this.damage = damage;
        targetPosition = targetMonster.transform.position;
    }
}
