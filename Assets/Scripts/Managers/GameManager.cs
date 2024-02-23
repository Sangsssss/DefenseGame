using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public RTSUnitController rTSUnitController;
    public MonsterSpawner monsterSpawner;
    private AudioSource backgroundMusic;
    [SerializeField] private AudioClip shuffleCardSound;
    [SerializeField] private AudioClip drawCardSound;
    [SerializeField] private AudioClip upgradeSound;
    public enum GameStatus {
        Start, Spawn, Ready, GameOver
    }
    private enum GameSpeed {
        Normal, X2
    }
    public GameStatus gameStatus;

 
    public bool isStart = false;
    public bool isGameover = false;

    public float startTime = 5f;
    
    public int goldCount;
    public int currentLife;
    public int life;

    public Enums.SpendType spendType;
    private GameSpeed gameSpeed;
    
    private Dictionary<Enums.SpendType, int> spendCosts;

    // 싱글톤 구성
    public static GameManager instance {
        get {
            if(m_instance == null) {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    // Start is called before the first frame update
    private static GameManager m_instance;

    void Awake() {
        if(instance != this) {
            Destroy(gameObject);
        }
        backgroundMusic = this.GetComponent<AudioSource>();
        gameStatus = GameStatus.Start;
        goldCount = 100;
        life = 20;
        currentLife = life;

        spendCosts = new Dictionary<Enums.SpendType, int>()
        {
        { Enums.SpendType.DRAW, 2 },
        { Enums.SpendType.SHUFFLE, 2 },
        { Enums.SpendType.FIREUPGRADE, 1},
        { Enums.SpendType.ICEUPGRADE, 1},
        { Enums.SpendType.LIGHTUPGRADE, 1},
        { Enums.SpendType.DARKNESSUPGRADE, 1}
        };
    }

    void Start()
    {   
        UIManager.instance.StartGame(); 
        UIManager.instance.UpdateGold(goldCount);  
        UIManager.instance.UpdateLife((currentLife + "/" + life).ToString());  
        backgroundMusic.Play();    
    }

    // Update is called once per frame
    void Update()
    {   
        // 스폰 시작
        if(Time.time >= startTime && !isStart) {
            UIManager.instance.StartSpawn();
            isStart = true;
        }
    }

    public void GainGold(int gold) {
        // 웨이브마다 골드 획득
        goldCount += gold;
        UIManager.instance.UpdateGold(goldCount);
    }

    public void PlayMonsterDeathSound(AudioClip monsterDeathSound) {
        backgroundMusic.PlayOneShot(monsterDeathSound);
    }

     public void PlayUnitAttackSound(AudioClip unitAttackSound) {
        backgroundMusic.PlayOneShot(unitAttackSound);
    }

    public void PlayUpgradeSound() {
        backgroundMusic.PlayOneShot(upgradeSound);
    }

    //
    public bool UseGold(Enums.SpendType type) {
        int cost = 0;
        if(type != Enums.SpendType.DRAW && type != Enums.SpendType.SHUFFLE) {
            cost = spendCosts[type]++;
        } else {
            cost = spendCosts[type];
        }
        if(goldCount < cost) {
            UIManager.instance.LackOfGold();
            return false;
        } else {
            goldCount -= cost;
            UIManager.instance.UpdateGold(goldCount);
            return true;
        }
    }

    public void LoseLife(int damage) {
        currentLife -= damage;
        UIManager.instance.UpdateLife((currentLife + "/" + life).ToString());
    }
    

    public void EndGame()
    {   
        isGameover = true;
        UIManager.instance.EndGame();
    }

    public void UpdateUpgrade(Enums.SpendType type) {
        UIManager.instance.UpdateUpgrade((int) type, spendCosts[type]);
    }

    public bool ShuffleCard() {
        if(UseGold(Enums.SpendType.SHUFFLE)) {
            backgroundMusic.PlayOneShot(shuffleCardSound);
            return true;
        } return false;
    }  

    public void FlipSpawnCard() {
        backgroundMusic.PlayOneShot(drawCardSound);
        UseGold(Enums.SpendType.DRAW);
    }

    public void FlipRewardCard() {
        backgroundMusic.PlayOneShot(drawCardSound);
    }

    // 일반 속도, 2배 속도
    public void GameSpeedUp() {
        if(gameSpeed != GameSpeed.X2) {
            gameSpeed = GameSpeed.X2;
            //1. Unit Controller에서 유닛의 이동속도 및 공격속도를 2배로
            rTSUnitController.UpUnitSpeed();
            //2. Monster Spawner에서 Monster의 이동속도를 2배로
        }
 
    }
    public void GameSpeedDown() {
        if(gameSpeed != GameSpeed.Normal) {
            gameSpeed = GameSpeed.Normal;
            //1. Unit Controller에서 유닛의 이동속도 및 공격속도를 2배로
            rTSUnitController.DownUnitSpeed();
            //2. Monster Spawner에서 Monster의 이동속도를 2배로
        }
    }

}
