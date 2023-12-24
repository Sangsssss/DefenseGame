using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class RewardRune : MonoBehaviour
{
    [SerializeField] private ERewardType eRewardType;
    public ERewardType RewardType {get {return eRewardType;}}
    [SerializeField] private Image runeImage;
    [SerializeField] private TMP_Text runeName;
    public string RuneName { get {return runeName.text;}}
    [SerializeField] private TMP_Text runeDescription;
    public string RuneDescription { get {return runeDescription.text;}}
    [SerializeField] private int grade;
    public int Grade {get {return grade;}}

    public RewardRuneData rewardRuneData;

    public void SetUpCard(RewardRuneData rewardRuneData) {
        this.rewardRuneData = rewardRuneData;
        eRewardType = this.rewardRuneData.eRewardType; 
        Debug.Log("reward Type :" + eRewardType);   
        runeImage.sprite = this.rewardRuneData.runeSprite;
        runeName.text = this.rewardRuneData.runeName;
        Debug.Log("Rune Name :" + runeName.text); 
        runeDescription.text = this.rewardRuneData.runeDescription;
        Debug.Log("Rune Description :" + runeDescription.text); 
        grade = this.rewardRuneData.grade;
        Debug.Log("Grade" + grade);
    }
}

