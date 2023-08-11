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
        goldCount = 6;
        UIManager.instance.StartGame(); 
        UIManager.instance.UpdateGold(goldCount);  
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

    public void UseGold() {
        goldCount -= 2;
        UIManager.instance.UpdateGold(goldCount);
    }

    public void EndGame()
    {   
        isGameover = true;
        UIManager.instance.EndGame();
    }

}
