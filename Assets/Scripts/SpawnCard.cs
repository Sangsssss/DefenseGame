using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCard : MonoBehaviour
{   
    [SerializeField] private Enums.EUnitAttribute attributeType;
    public Enums.EUnitAttribute AttributeType {get {return attributeType;}}
    [SerializeField] private Sprite attributeSprite;
    public Sprite AttributeSprite {get {return attributeSprite;}}
    [SerializeField] private Sprite unitSprite;
    public Sprite UnitSprite {get {return unitSprite;}}
    [SerializeField] private TMP_Text gold;
    public string Gold {get {return gold.text;}}
    [SerializeField] private int grade;
    public int Grade {get {return grade;}}

    public SpawnCardData spawnCardData;

    void Awake() {
        attributeSprite = this.GetComponent<Image>().sprite;
    }

    public void SetUpCard(SpawnCardData spawnCardData) {
        this.spawnCardData = spawnCardData;
        attributeType = this.spawnCardData.eUnitAttribute; 
        Debug.Log(attributeType);   
        attributeSprite = this.spawnCardData.attributeCardSprite;
        Debug.Log(attributeSprite); 
        unitSprite = this.spawnCardData.unitSprite;
        Debug.Log(unitSprite); 
        gold.text = this.spawnCardData.gold.ToString();
        Debug.Log(gold.text); 
        //grade = spawnCardSO.Grade;
    }
}
