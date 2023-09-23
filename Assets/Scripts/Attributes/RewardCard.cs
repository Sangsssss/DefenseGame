using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardCard : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private RewardCardData rewardCardData;

   public RewardCardData RewardCardData { get {return rewardCardData;}}


    void Start() {

    }
}

