using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MonsterSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    // public Enemy enemyPrefab;s
    // public Enemy enemyPrefab2;

    private MonsterList monsterList;

    public Transform spawnPoint;
    public Transform[] wayPoints;

    private List<Monster> Monsters = new List<Monster>();
   
    
    private int wave = 1;
    private int endWave = 20;
    private float spawnTime = 0;
    private int spawnCount = 0;

    public int normalSpawnCount = 5;
    public int bossSpawnCount = 1;

    private List<int> aliveCount;
    
    [Range(0f, 20f)]
    public float nextWave;
    [Range(0f, 2f)]
    public float nextSpawn;


    private enum RoundType {
        Normal = 5,
        Boss = 1
    }

    private RoundType roundType;

    
    void Awake() {
         monsterList = this.GetComponent<MonsterList>();
    }

    void Start()
    {   
        Monsters = new List<Monster>();
        aliveCount = new List<int>();
        for(int i = 0; i<endWave; i++) {
            aliveCount.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {   
         if (!GameManager.instance.isStart || GameManager.instance.isGameover)
        {
             return; // 게임이 시작되지 않았거나 게임오버 상태인 경우 아무 작업도 하지 않음
        }

        // 1. Win Process ==> 웨이브가 끝나고, 몬스터가 모두 사망했을 시 게임 종료
        // 2. Lose Process ==> Player의 life가 모두 소멸 될 시.. 

        // ===> required Detail Process 
        if(wave > monsterList.monsterPrefabs.Count && Monsters.Count <= 0) {   
            GameManager.instance.EndGame();
            Debug.Log("게임 종료");
        } 

        UpdateUI();
        if(Time.time >= spawnTime) {
                spawnTime = Time.time + nextWave;
                SpawnWave();
                UIManager.instance.UpdateWave(wave);
                Debug.Log("wave : " + wave);
            }   
       
        //  if (GameManager.instance != null && GameManager.instance.isGameover)
        // {
        //     return;
        // }

    }

    public void StartSpawning()
    {   
        spawnTime = Time.time;
        SpawnWave();
    }

     private void UpdateUI() {
        // 현재 웨이브와 남은 적의 수 표시
        int count = (Monsters != null) ? Monsters.Count : 0;
        UIManager.instance.UpdateEnemyCount(count);
    }


     private void SpawnWave() {
            StartCoroutine(CreateEnemy());          
    }


     public IEnumerator CreateEnemy() {
        //1. CheckRoundType() => wave를 통해 일반 라운드인지, 보스라운드인지 확인 => setRoundType()
        //2. spawnCount = RoundType.Boss
        CheckRoundType();
        
        // BOSS-R 종료 후, 플레이어에게 Card 선택권 부여
        for(int i = 0; i < spawnCount; i++) {
            MonsterData monsterData = monsterList.monsters[wave-1];
            Monster monster = Instantiate(monsterData.monsterPrefab, spawnPoint.position, spawnPoint.rotation);
            monster.SetUp(monsterData.health, monsterData.damage ,monsterData.gold, wayPoints);
            Monsters.Add(monster);

            monster.OnAttack += () =>
            {
                OnMonsterAttack(monster);
            };
            
            monster.OnDeath += () =>
            {
                OnMonsterDeath(monsterData, monster);
            };
            
            Debug.Log("적군 생성 => " + monsterData.monsterName + " 체력 :" + monsterData.health);
            yield return new WaitForSeconds(nextSpawn);
        }
        wave++;
    }

    // 몬스터가 플레이거 공격 시
    private void OnMonsterAttack(Monster monster)
    {
        Monsters.Remove(monster);
        GameManager.instance.LoseLife(monster.damage);
        Destroy(monster.gameObject);
    }
    // 몬스터가 플레이어에게 사망 시
    private void OnMonsterDeath(MonsterData monsterData, Monster monster)
    {
        Monsters.Remove(monster);
        Destroy(monster.gameObject);
        aliveCount[monsterData.wave - 1]--;
        if (aliveCount[monsterData.wave - 1] == 0)
        {
            if (monsterData.wave % 3 == 0)
            {
                // 카드 뽑기
                CardManager.Instance.DrawRewards();
            }
            GameManager.instance.GainGold(monster.gold);
        }
    }

    private void CheckRoundType()
    {    
        if(wave % 3 == 0) {
             spawnCount = (int)RoundType.Boss;
        } else {
             spawnCount = (int)RoundType.Normal;
        }
        aliveCount[wave-1] = spawnCount;
    }
}
