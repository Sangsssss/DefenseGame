using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardCard : Card
{
    [SerializeField] private Enums.ERewardType eRewardType;
    public Enums.ERewardType RewardType {get {return eRewardType;}}
    [SerializeField] private Image gradeCardSpriteImg;
    private Sprite gradeCardSprite;
    public Sprite GradeCardSprite {get {return gradeCardSprite;}}
    [SerializeField] private Image typeSpriteImg;
    private Sprite typeSprite;
    public Sprite TypeSprite {get {return typeSprite;}}
    [SerializeField] private int grade;
    public int Grade {get {return grade;}}

    public RewardCardData rewardCardData;

    public void SetUpCard(RewardCardData rewardCardData) {
        this.rewardCardData = rewardCardData;
        eRewardType = this.rewardCardData.eRewardType; 
        Debug.Log("reward Type :" + eRewardType);   
        gradeCardSpriteImg.sprite = this.rewardCardData.gradeCardSprite;
        Debug.Log("gradeCard Sprite :" + gradeCardSprite); 
        typeSpriteImg.sprite = this.rewardCardData.typeSprite;
        Debug.Log("type Sprite :" + typeSprite); 
        grade = this.rewardCardData.grade;
        Debug.Log("Grade" + grade);
    }
}

