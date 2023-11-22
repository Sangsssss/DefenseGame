using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnCard : Card
{   
    [SerializeField] private Enums.EUnitAttribute attributeType;
    public Enums.EUnitAttribute AttributeType {get {return attributeType;}}

    [SerializeField] private Image attributeFrameColor;
    [SerializeField] private Image backGlowColor;
    [SerializeField] private Image glowColor;
 
    [SerializeField] private Image characterImage;  
    public Sprite CharacterSprite {get {return characterImage.sprite;}}

    [SerializeField] private Image typeImage; 
    public Sprite TypeSprite {get {return typeImage.sprite;}}
    
    
    [SerializeField] private TMP_Text unitName;
    public string UnitName {get {return unitName.text;}}
    [SerializeField] private TMP_Text gold;
    public string Gold {get {return gold.text;}}

    [SerializeField] private int grade;
    public int Grade {get {return grade;}}
    [SerializeField] private Image[] stars;

    // public int Grade {get {return grade;}}

    public SpawnCardData spawnCardData;


    public void SetUpCard(SpawnCardData spawnCardData) {
        this.spawnCardData = spawnCardData;
        attributeType = this.spawnCardData.eUnitAttribute; 

        attributeFrameColor.color = this.spawnCardData.attributeFrameColor;
        backGlowColor.color = this.spawnCardData.backGlowColor;
        glowColor.color = this.spawnCardData.glowColor;

        characterImage.sprite = this.spawnCardData.characterSprite;
      
        typeImage.sprite = this.spawnCardData.typeSprite;

        unitName.text = this.spawnCardData.unitName.ToString();

        gold.text = this.spawnCardData.gold.ToString();
        
        grade = this.spawnCardData.grade;

        for(int i = 0; i<spawnCardData.grade; i++) {
            stars[i].gameObject.SetActive(true);
        }
    }


}
