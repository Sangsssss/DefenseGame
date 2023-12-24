using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static Enums;

    [System.Serializable]
    public class RewardRuneData {
        public Sprite runeSprite;
        public string runeName;
        public string runeDescription;
        public ERewardType eRewardType;
        public int grade;

    }


    [CreateAssetMenu(fileName = "RewardRune Data", menuName = "Scriptable Object/RewardRune Data", order = int.MaxValue)]
    public class RewardRuneSO : ScriptableObject
    {

    [SerializeField] private RewardRuneData[] rewardRuneDatas;
    public RewardRuneData[] RewardRuneData {get {return rewardRuneDatas;}}

}
