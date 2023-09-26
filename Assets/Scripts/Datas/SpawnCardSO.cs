using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpawnCardData {
    public Sprite attributeCardSprite;
    public Sprite unitSprite;
    public Enums.EUnitAttribute eUnitAttribute;
    public int gold;
    public int grade;
}

[CreateAssetMenu(fileName = "SpawnCard Data", menuName = "Scriptable Object/SpawnCard Data", order = int.MaxValue)]
public class SpawnCardSO : ScriptableObject
{
    public SpawnCardData[] spawnCardDatas;
    [SerializeField] public SpawnCardData[] SpawnCardData {get {return spawnCardDatas;}}
    // [SerializeField] private Sprite attributeCardSprite;
    // public Sprite AttributeCardSprite {get {return attributeCardSprite;}}
    // [SerializeField] private Sprite unitSprite;
    // public Sprite UnitSprite {get {return unitSprite;}}
    // [SerializeField] private Enums.EUnitAttribute eUnitAttribute;
    // public Enums.EUnitAttribute EUnitAttribute {get {return eUnitAttribute;}}
    // [SerializeField] private int gold;
    // public int Gold {get {return gold;}}
    // [SerializeField] private int grade;
    // public int Grade {get {return grade;}}
}
