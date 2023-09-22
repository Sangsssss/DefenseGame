using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MonsterSpawner : MonoBehaviour
{

    [SerializeField] private List<MonsterData> monsterDatas;
    [SerializeField] private List<GameObject> monsterPrefabs;

    public Transform spawnPoint;

    private List<Monster> Monsters = new List<Monster>();
    private int wave = 1;
    private int endWave = 20;
    private float spawnTime = 0;
    private int spawnCount = 0;

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
    }

    void Start()
    {   
        Monsters = new List<Monster>();
        // 리스트를 만들고, 웨이브 수만큼 메모리 할당
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
        // if(wave > monsters.Count && Monsters.Count <= 0) {   
        //     GameManager.instance.EndGame();
        //     Debug.Log("게임 종료");
        // } 

        UpdateUI();
        if(Time.time >= spawnTime) {
                spawnTime = Time.time + nextWave;
                SpawnWave();
                UIManager.instance.UpdateWave(wave); // 여기서 변경해야 할듯?
            }     
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
        // 웨이븜다 
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
            // 데이터 입력
            Monster monster = Instantiate(monsterPrefabs[wave-1], spawnPoint.position, spawnPoint.rotation).GetComponent<Monster>();
            monster.MonsterData = monsterDatas[wave-1];
            if(monster != null) {
                monster.WatchMonsterInfo();
            } else {
                Debug.Log("error");
            }

            Monsters.Add(monster);

            monster.OnAttack += () =>
            {
                OnMonsterAttack(monster);
            };
            
            monster.OnDeath += () =>
            {   
                int cWave = wave-1;
                OnMonsterDeath(cWave, monster);
            };
            
            //Debug.Log("적군 생성 => " + monsterData.monsterName + " 체력 :" + monsterData.health);
            yield return new WaitForSeconds(nextSpawn);
        }
        wave++;
    }


    // extract Method 

    // 몬스터가 플레이거 공격 시
    private void OnMonsterAttack(Monster monster)
    {
        Monsters.Remove(monster);
        Destroy(monster.gameObject);
    }
    // 몬스터가 플레이어에게 사망 시
    private void OnMonsterDeath(int wave, Monster monster)
    {
        Monsters.Remove(monster);
        Destroy(monster.gameObject);
        aliveCount[wave]--;
        Debug.Log(wave + "의 남은 마릿 수 : " + aliveCount[wave]);
        if (aliveCount[wave] == 0)
        {
            if ((wave+1) % 3 == 0)
            {
                // 카드 뽑기
                CardManager.Instance.DrawRewards();
            }
            // 마지막 몬스터에게 돈을 주라고??
            monster.IsLast();
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
