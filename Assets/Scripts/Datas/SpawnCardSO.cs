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

}
