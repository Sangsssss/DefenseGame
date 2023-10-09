using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
    [System.Serializable]
    public class RewardCardData {
        public Sprite gradeCardSprite;
        public Sprite typeSprite;
        public Enums.ERewardType eRewardType;
        public int grade;

    }


    [CreateAssetMenu(fileName = "RewardCard Data", menuName = "Scriptable Object/RewardCard Data", order = int.MaxValue)]
    public class RewardCardSO : ScriptableObject
    {

    public RewardCardData[] rewardCardDatas;
    [SerializeField] public RewardCardData[] RewardCardData {get {return rewardCardDatas;}}

}
