using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster01 : MonoBehaviour
{
    [SerializeField]
    private MonsterData monsterData;
    public MonsterData MonsterData { get; set; }

    public void WatchMonsterInfo()
    {   
        Debug.Log("몬스터 이름 :: " + monsterData.name);
        Debug.Log("몬스터 wave :: " + monsterData.Wave);
        Debug.Log("몬스터 체력 :: " + monsterData.Health);
        Debug.Log("몬스터 획득골드 :: " + monsterData.Gold);
    }
}
