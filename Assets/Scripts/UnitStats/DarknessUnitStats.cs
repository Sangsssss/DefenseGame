using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessUnitStats : UnitStats
{
    // Start is called before the first frame update
    void Awake()
    {   
        Damage = 1f;
        AttackRange = 5f;
        AttackSpeed = 1.2f;
        Type = UnitType.Darkness;
    }

    // Update is called once per frame

}
