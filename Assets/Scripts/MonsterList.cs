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
        AddMonster("OneEyeSlime", monsterPrefabs[0], 10, 1);
        // Adding a Goblin monster to the list
        AddMonster("Slime", monsterPrefabs[1], 15, 2);
    }

    void AddMonster(string monsterName, Monster monsterPrefab, int health, int gold) {
        monsters.Add(new MonsterData {
            monsterName = monsterName,
            monsterPrefab = monsterPrefab,
            health = health,
            gold = gold
        });  
    }

}

public class MonsterData
{
    public string monsterName;
    public Monster monsterPrefab;
    public int health;
    public int gold;
}