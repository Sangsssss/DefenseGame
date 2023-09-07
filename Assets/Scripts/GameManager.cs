using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioSource backgroundMusic;

    public bool isStart = false;

    public bool isGameover = false;
    public float startTime = 5f;
    
    public int goldCount;
    public int life;

    public int drawGold = 2;

    // 총 4가지 타입의 강화
    private int[] steps = new int[4];


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
    }

    void Start()
    {   
        for(int i = 0; i<steps.Length; i++) {
            steps[i] = 1;
        }
        goldCount = 6;
        life = 20;
        UIManager.instance.StartGame(); 
        UIManager.instance.UpdateGold(goldCount);  
        UIManager.instance.UpdateLife(life);  
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
        goldCount += gold;
        UIManager.instance.UpdateGold(goldCount);
    }


    //
    public void UseGold() {
        goldCount -= drawGold;
        UIManager.instance.UpdateGold(goldCount);
    }

    public void LoseLife(int damage) {
        life -= damage;
        UIManager.instance.UpdateLife(life);
    }


    public void EndGame()
    {   
        isGameover = true;
        UIManager.instance.EndGame();
    }

    public void UpdateUpgrade(int index) {
        steps[index] += 1;
        Debug.Log(steps[index]);
        UIManager.instance.UpdateUpgrade(index, steps[index]);
    }

}
