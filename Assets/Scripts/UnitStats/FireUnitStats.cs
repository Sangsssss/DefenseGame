using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데이터를 분리하는 것이 맞을까??
// object로 부터 가져오기 
public class FireUnitStats : UnitStats
{
    FireUnitStats[] fireUnitStats;
    // private void Awake() {
    //     fireUnitStats = new FireUnitStats[5];
    //     fireUnitStats[0].Grade = unitStatSO.UnitStatData[0].grade;
    //      Grade =  unitStatSO.UnitStatData[0].grade;
    //      Damage =  unitStatSO.UnitStatData[0].Damage;
    //      AttackRange =  unitStatSO.UnitStatData[0].AttackRange;
    //      AttackSpeed =  unitStatSO.UnitStatData[0].AttackSpeed;
    //      EUnitAttribute =  unitStatSO.UnitStatData[0].eUnitAttribute;
    // }

    public override void UpgradeDamage()
    {   
        base.UpgradeDamage();
    }

    // 가시적으로 스탯을 넣을 수 있는 방법은?

}
