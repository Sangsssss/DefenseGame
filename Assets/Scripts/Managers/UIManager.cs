using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using System.Transactions;
using UnityEditor.UIElements;
using System;
using System.Linq;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수


    [Header ("Player Status")]
    [SerializeField] private Text waveText;
    [SerializeField] private Text timeText;
    [SerializeField] private TMP_Text enemyCntText;
    [SerializeField] private TMP_Text gold;
    [SerializeField] private TMP_Text life;


    [Header ("Unit Count")]
    [SerializeField] private TMP_Text fireUnitCount;
    [SerializeField] private TMP_Text IceUnitCount;
    [SerializeField] private TMP_Text LightUnitCount;
    [SerializeField] private TMP_Text DarknessUnitCount;

    [Header ("Wave Status")]
    [SerializeField] private Image[] waveStatus; 
    [SerializeField] private Sprite unCheckWave;
    [SerializeField] private Sprite checkWave;
    [SerializeField] private Sprite unCheckKing;
    [SerializeField] private Sprite checkKing;
    [SerializeField] private Sprite progressWave;
    [SerializeField] private Sprite progressKing;

    [Header ("Reward")]
    [SerializeField] private Image rewardPanel;
    
    [Header ("Spawn")]
    [SerializeField] private GameObject spawnCardPanel;

    [Header ("Upgrade")]
    [SerializeField] private Text[] upgradeStep;

    [Header ("Ohter")]
    public TMP_Text informationText;

    
   // [SerializeField] private CardManager cardPanel;
   

    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {   

        int minutes = Mathf.FloorToInt(Time.time / 60f);
        int seconds = Mathf.FloorToInt(Time.time % 60f);

        // mm:ss 형태의 문자열로 포맷팅합니다.
        timeText.text  = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateGold(int goldCount) {
        gold.text = goldCount.ToString();
    }

    public void UpdateLife(int lifeCount) {
        life.text = lifeCount.ToString();
    }

    public void UnitSpawn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateWave(int wave) {
        // Wave Status 초기화
        if(wave%5 == 0) {
            for(int i = 0; i < waveStatus.Length-1; i++) {
                waveStatus[i].sprite = unCheckWave;
            }
            waveStatus[waveStatus.Length-1].sprite = progressWave;
        } else {
            if(wave%6-1 == 4) {
                waveStatus[wave%6-1].sprite = progressKing;
            } else {
                waveStatus[(wave%6)-1].sprite = progressWave;
            }
        }
        waveText.text = "WAVE : " + wave;
    }

    public void CompleteWave(int wave) {
        // 보스 웨이브
        if((wave%6)-1 == 4) {
            waveStatus[wave%6-1].sprite = checkKing;
        }
        // 노말 웨이브  
        else {
             waveStatus[(wave%6)-1].sprite = checkWave;
        }
    }
    
    //남아있는 몬스터 수 표시
    public void UpdateEnemyCount(int enemyCount) {
        enemyCntText.text = "Count : " + enemyCount;
    }

    public void StartGame() {
        informationText.gameObject.SetActive(true);
        informationText.text = "Monster Spawn Soon!";
        
    }

    public void StartSpawn() {
        informationText.enabled = false;
    }

    public void EndGame() {
       // Debug.Log("게임 종료");
        informationText.text = "Game Clear!!";
        Debug.Log(informationText.text);
        informationText.enabled = true;
    }   

    public void LackOfGold() {
        StartCoroutine(DisabledTextAfterDelay());
    }

    IEnumerator DisabledTextAfterDelay() {
        informationText.text = "Lack Of GOld!";
        informationText.enabled = true;
        yield return new WaitForSeconds(1.5f);
        informationText.enabled = false;
    }


    public void UpdateUnitStatus(Enums.EUnitAttribute attribute, int unitCount) {
        if(attribute == Enums.EUnitAttribute.FIRE) {
            fireUnitCount.text = unitCount.ToString();
        } else if(attribute == Enums.EUnitAttribute.ICE) {
            IceUnitCount.text = unitCount.ToString();
        } else if(attribute == Enums.EUnitAttribute.LIGHT) {
            LightUnitCount.text = unitCount.ToString();
        } else {
            DarknessUnitCount.text = unitCount.ToString();
        }
       
    }

    public void UpdateUpgrade(int index, int newStep) {
        upgradeStep[index].text = newStep.ToString();
    }

    public void DrawRewardPanel()
    {   
        rewardPanel.gameObject.SetActive(true);
    }

    public void RemoveRewardPanel()
    {   
        rewardPanel.gameObject.SetActive(false);
    }

    public void VisibleUI() {
        Debug.Log("UI ON");
        spawnCardPanel.gameObject.SetActive(true);
    }

    

}
