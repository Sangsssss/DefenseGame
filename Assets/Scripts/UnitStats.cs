using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{   

    public enum UnitType {
        Fire, Ice, Light, Darkness
    }
    // 유닛이 가지고 있는 Stat
    // 등급(1,2,3,4,5) & 데미지 & 공격 범위 & 공격 속도 &  
    public int Grade { get; protected set;}
    public float Damage {get; set;}
    public float AttackRange {get; protected set;}
    public float AttackSpeed {get; protected set;}
    public UnitType Type {get; protected set;}
    
    public virtual void UpgradeDamage() {
        Damage = (float)(Damage * 1.2);
    }
}
