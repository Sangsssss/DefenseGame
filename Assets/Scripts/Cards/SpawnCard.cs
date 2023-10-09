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
    [SerializeField] private Image attributeSpriteImg;
    private Sprite attributeSprite;
    public Sprite AttributeSprite {get {return attributeSprite;}}
    [SerializeField] private Image unitSpriteImg;
    private Sprite unitSprite;
    public Sprite UnitSprite {get {return unitSprite;}}
    [SerializeField] private TMP_Text gold;
    public string Gold {get {return gold.text;}}
    [SerializeField] private int grade;
    public int Grade {get {return grade;}}

    public SpawnCardData spawnCardData;

    void Awake() {
        // attributeSprite = attributeSpriteImg.sprite;
        // unitSprite = unitSpriteImg.sprite;
    }

    public void SetUpCard(SpawnCardData spawnCardData) {
        this.spawnCardData = spawnCardData;
        attributeType = this.spawnCardData.eUnitAttribute; 
        Debug.Log("Attribute Type :" + attributeType);   
        attributeSpriteImg.sprite = this.spawnCardData.attributeCardSprite;
        Debug.Log("Attribute Sprite :" + attributeSprite); 
        unitSpriteImg.sprite = this.spawnCardData.unitSprite;
        Debug.Log("Unit Sprite :" + unitSprite); 
        gold.text = this.spawnCardData.gold.ToString();
        Debug.Log("Gold :" +gold.text); 
        grade = this.spawnCardData.grade;
        Debug.Log("Grade" +spawnCardData.grade);
    }
}
