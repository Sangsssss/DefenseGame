using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardAttribute : MonoBehaviour
{
    // Start is called before the first frame update
    public enum RewardType {
        Gold, Stat, Unit
    }

   public RewardType rewardType;
}
