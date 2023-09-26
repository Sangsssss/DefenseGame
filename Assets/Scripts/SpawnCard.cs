using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCard : MonoBehaviour
{   

    [SerializeField] private SpriteRenderer attribute;
    [SerializeField] private Sprite unitSprite;
    [SerializeField] private TextMeshPro gold;
    [SerializeField] private int grade;

    public SpawnCardData spawnCardData;

    public void SetUpCard(SpawnCardData spawnCardData) {
        this.spawnCardData = spawnCardData;
        this.GetComponent<Image>().sprite = spawnCardData.attributeCardSprite;
        unitSprite = spawnCardData.unitSprite;
        gold.text = spawnCardData.gold.ToString();
        //grade = spawnCardSO.Grade;
    }
}
