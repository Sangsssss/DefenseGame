using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardCard Data", menuName = "Scriptable Object/RewardCard Data", order = int.MaxValue)]
public class RewardCardData : ScriptableObject
{
    



    public enum RewardType {
        GOLD, UPGRADE, UNIT
    }
    [SerializeField] private RewardType rewardType;
    public RewardType Type { get {return rewardType;} }

    [SerializeField] private int reward;
    public int Reward { get {return reward;}}

}
