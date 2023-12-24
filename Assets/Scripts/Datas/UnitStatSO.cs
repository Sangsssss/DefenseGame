using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[System.Serializable]
public class UnitStatData {
    [SerializeField] private string name;
    public string Name {get {return name;} set {name = value;}}

    [SerializeField] private int grade;
    public int Grade {get {return grade;} set {grade = value;}}

    [SerializeField] private double damage;
    public double Damage {get {return damage;} set {damage = value;}}

    [SerializeField] private float attackRange;
    public float AttackRange {get {return attackRange;} set {attackRange = value;} }

    [SerializeField] private float attackSpeed;
    public  float AttackSpeed {get {return attackSpeed;} set {attackSpeed = value;} }
    [SerializeField] private EUnitAttribute eUnitAttribute;
    public EUnitAttribute EUnitAttribute {get {return eUnitAttribute;} set {eUnitAttribute = value;} }

    
    // Copy constructor
    public UnitStatData(UnitStatData original)
    {   
        Name = original.Name;
        Grade = original.Grade;
        Damage = original.Damage;
        AttackRange = original.AttackRange;
        AttackSpeed = original.AttackSpeed;
        EUnitAttribute = original.EUnitAttribute;
    }
    public void UpgradeDamage() {
        damage = damage * grade * 1.1;
    }
}



[CreateAssetMenu(fileName = "UnitStat Data", menuName = "Scriptable Object/UnitStat Data", order = int.MaxValue)]
public class UnitStatSO : ScriptableObject
{
    [SerializeField] private UnitStatData[] unitStatDatas;
    public UnitStatData[] UnitStatData {get {return unitStatDatas;}}

}
