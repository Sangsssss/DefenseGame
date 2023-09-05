using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUnitStats : UnitStats
{
    // Start is called before the first frame update
    void Awake()
    {   
        Grade = 1;
        Damage = 1f;
        AttackRange = 5f;
        AttackSpeed = 1.2f;
        Type = UnitType.Light;
    }

    // Update is called once per frame
       public override void UpgradeDamage()
    {
        base.UpgradeDamage();
    }
}
