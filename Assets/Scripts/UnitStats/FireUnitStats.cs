using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUnitStats : UnitStats
{
    // Start is called before the first frame update
    void Awake()
    {   
        Grade = 1;
        Damage = 1f;
        AttackRange = 5f;
        AttackSpeed = 1.2f;
        Type = UnitType.Fire;
    }

    // Update is called once per frame
    public override void UpgradeDamage()
    {   
        base.UpgradeDamage();
    }

}
