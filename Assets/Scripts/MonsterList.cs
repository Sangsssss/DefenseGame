using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterList : MonoBehaviour
{
     public List<MonsterData> monsters;
     public List<Monster> monsterPrefabs;

     void Start()
    {
        monsters = new List<MonsterData>();

        // Adding a Slime monster to the list
        AddMonster(1, "OneEyeSlime", monsterPrefabs[0], 10, 1 ,1);
        // Adding a Goblin monster to the list
        AddMonster(2, "Slime", monsterPrefabs[1], 15, 2 , 2);
        AddMonster(3, "KingSlime", monsterPrefabs[2], 30, 20, 4);
    }

    void AddMonster(int wave, string monsterName, Monster monsterPrefab, int health, int damage, int gold) {
        monsters.Add(new MonsterData {
            wave = wave,
            monsterName = monsterName,
            monsterPrefab = monsterPrefab,
            health = health,
            damage = damage,
            gold = gold
        });  
    }

}

public class MonsterData
{   
    public int wave;
    public string monsterName;
    public Monster monsterPrefab;
    public int health;
    public int damage;
    public int gold;
}