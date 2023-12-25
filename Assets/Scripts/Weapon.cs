using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    private Transform muzzlePoint;
    [SerializeField] private AudioClip shootingSound;
    private GameObject projectilePrefab;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {   
        anim = this.GetComponent<Animator>();
        muzzlePoint = transform.Find("Muzzle_Point");

        if (muzzlePoint == null)
        {
            Debug.LogError("MuzzlePoint is Null!!");
        }
    }


    public void SetUp(GameObject projectilePrefab) {
        this.projectilePrefab = projectilePrefab;
    }
    public void Shooting(Monster targetMonster, double damage) {

        GameManager.instance.PlayUnitAttackSound(shootingSound);
        if(anim != null) {
            anim.SetTrigger("Shoot");
        }
        // Vector3 targetDirection = (targetMonster.transform.position - transform.position).normalized;
        // Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);  

        GameObject projectileInstacne = Instantiate(projectilePrefab, muzzlePoint.position, transform.rotation);

        ProjectileMover projectile = projectileInstacne.GetComponent<ProjectileMover>();
        projectile.SetUp(targetMonster, damage);
    }
}
