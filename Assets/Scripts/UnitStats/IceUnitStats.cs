using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceUnitStats : UnitStats
{
    // Start is called before the first frame update
    //  private void Awake() {
    //      Grade =  unitStatSO.UnitStatData[1].grade;
    //      Damage =  unitStatSO.UnitStatData[1].Damage;
    //      AttackRange =  unitStatSO.UnitStatData[1].AttackRange;
    //      AttackSpeed =  unitStatSO.UnitStatData[1].AttackSpeed;
    //      EUnitAttribute =  unitStatSO.UnitStatData[1].eUnitAttribute;
    // }

    // Update is called once per frame
       public override void UpgradeDamage()
    {
        base.UpgradeDamage();
    }
}
