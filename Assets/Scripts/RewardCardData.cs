using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCardData : MonoBehaviour
{
   
    public enum RewardType {
        Gold, Stat, Unit
    }
    [SerializeField] private RewardType rewardType;
    public RewardType Type { get; set; }

    [SerializeField] private int grade;
    public int Grade { get; set; }

}
