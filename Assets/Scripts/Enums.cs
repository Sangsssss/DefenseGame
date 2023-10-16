using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enums : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EUnitAttribute {
     FIRE = SpendType.FIREUPGRADE, 
     ICE = SpendType.ICEUPGRADE, 
     LIGHT = SpendType.LIGHTUPGRADE,
     DARKNESS = SpendType.DARKNESSUPGRADE
    }

    public enum ERewardType {
        GOLD, UNIT, STAT
    }

    public enum SpendType
    {
                
    // 업그레이드 단계마다 사용하는 골드 다르게 해야함.
    FIREUPGRADE,
    ICEUPGRADE,
    LIGHTUPGRADE,
    DARKNESSUPGRADE,
    DRAW,   // Draw에 대한 가격은 1
    SHUFFLE // Shuffle에 대한 가격은 2



    }


}
