using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

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
    private float spawnTime = 0;
    private int spawnCount = 0;

    public int normalSpawnCount = 5;
    public int bossSpawnCount = 1;
    
    [Range(0f, 20f)]
    public float nextWave;
    [Range(0f, 2f)]
    public float nextSpawn;

    
    void Awake() {
         monsterList = this.GetComponent<MonsterList>();
    }

    void Start()
    {   
        Monsters = new List<Monster>();
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
        if(wave > monsterList.monsterPrefabs.Count || GameManager.instance.life <= 0) {   
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
        if(wave % 3 == 0 ) {
            spawnCount = bossSpawnCount;
        } else {
            spawnCount = normalSpawnCount;
        }

        for(int i = 0; i < spawnCount; i++) {
            MonsterData monsterData = monsterList.monsters[wave-1];
            Monster monster = Instantiate(monsterData.monsterPrefab, spawnPoint.position, spawnPoint.rotation);
            monster.SetUp(monsterData.health, monsterData.damage ,monsterData.gold, wayPoints);
            Monsters.Add(monster);

            monster.OnAttack += () => {
                Monsters.Remove(monster);
                GameManager.instance.LoseLife(monster.damage);
                Destroy(monster.gameObject);
                // 플레이어 공격
            };
            
            monster.OnDeath += () => {
                Monsters.Remove(monster);
                Destroy(monster.gameObject);
                GameManager.instance.GainGold(monster.gold);
                
            };

            Debug.Log("적군 생성 => " + monsterData.monsterName + " 체력 :" + monsterData.health);
            yield return new WaitForSeconds(nextSpawn);
        }
        wave++;
    }
}
