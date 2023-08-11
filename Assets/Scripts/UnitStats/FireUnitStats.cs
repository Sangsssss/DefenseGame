using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUnitStats : UnitStats
{
    // Start is called before the first frame update
    void Awake()
    {   
        Damage = 1f;
        AttackRange = 5f;
        AttackSpeed = 1.2f;
        Attribute = "fire";
    }

    // Update is called once per frame

}
