using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class UnitStats : MonoBehaviour
{   
    [SerializeField] private Enums.EUnitAttribute eUnitAttribute;
    public Enums.EUnitAttribute EUnitAttribute {get {return eUnitAttribute;} set {eUnitAttribute = value;} }
    // 유닛이 가지고 있는 Stat
    // 등급(1,2,3,4,5) & 데미지 & 공격 범위 & 공격 속도 &  
    [SerializeField]  private string unitName;
    public string Name {get {return unitName;} set {unitName = value;}}
    [SerializeField]  private int grade;
     public int Grade {get {return grade;} set {grade = value;}}
    [SerializeField] private double damage;
    public double Damage {get {return damage;} set {damage = value;}}
    [SerializeField] private float attackRange;
    public float AttackRange {get {return attackRange;} set {attackRange = value;}}
    [SerializeField] private float attackSpeed;
    public float AttackSpeed {get {return attackSpeed;} set {attackSpeed = value;}}

    // 필드 위에 이미 생성된 유닛의 데미지를 업그레이드
    public virtual void UpgradeDamage() {
        damage = damage * grade * 1.1;
    }

    // 유닛이 필드 위에 생성될 때, UnitStatData를 참조해 유닛을 생성
    public virtual void SetUpUnitStat(UnitStatData newUnitStat) {
        unitName = newUnitStat.Name;
        eUnitAttribute = newUnitStat.EUnitAttribute;
        grade = newUnitStat.Grade;
        damage = newUnitStat.Damage;
        attackRange = newUnitStat.AttackRange;
        attackSpeed = newUnitStat.AttackSpeed;
    }

}
