using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardCard : MonoBehaviour
{
    // Start is called before the first frame update
   public enum UnitAttribute {
     FIRE, ICE, LIGHT, DARKNESS
   }
   private Enums.EUnitAttribute eUnitAttribute;
   [SerializeField] private UnitAttribute unitAttribute;
    public UnitAttribute Attribute {get {return unitAttribute;}}


   [SerializeField] private RewardCardData rewardCardData;
   public RewardCardData RewardCardData { get {return rewardCardData;}}


    void Start() {
        unitAttribute = this.RandomType();
    }

    private UnitAttribute RandomType() {
        float randomValue = UnityEngine.Random.value; // 0과 1 사이의 랜덤한 값을 가져옵니다.
        if (randomValue < 0.25f)
            return UnitAttribute.FIRE;
        else if (randomValue < 0.5f)
            return UnitAttribute.ICE;
        else if (randomValue < 0.75f)
            return UnitAttribute.LIGHT;
        else    
            return UnitAttribute.DARKNESS;
    }
}

