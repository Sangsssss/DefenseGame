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
using System.IO.Compression;
using System.Net;

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


    [Header ("Wave Status")]
    [SerializeField] private TMP_Text timeText;

    [Header ("Player Status")]
    [SerializeField] private TMP_Text enemyCntText;
    [SerializeField] private TMP_Text gold;
    [SerializeField] private TMP_Text life;


    [Header ("Unit Count")]
    [SerializeField] private TMP_Text fireUnitCount;
    [SerializeField] private TMP_Text IceUnitCount;
    [SerializeField] private TMP_Text LightUnitCount;
    [SerializeField] private TMP_Text DarknessUnitCount;

    [Header ("Wave Status")]
    [SerializeField] private GameObject[] checkWave;
 

    [Header ("Reward")]
    [SerializeField] private Image rewardPanel;
    
    [Header ("Spawn")]
    [SerializeField] private GameObject spawnCardPanel;

    [Header ("Upgrade")]
    [SerializeField] private TMP_Text[] upgradeStep;
    
    [Header ("Unit Stat")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private TMP_Text damageText;

    [Header ("Ohter")]
    [SerializeField] private GameObject information;
    [SerializeField] private TMP_Text informationText;
    [SerializeField] private GameObject settingPanel;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {   

        int minutes = Mathf.FloorToInt(Time.time / 60f);
        int seconds = Mathf.FloorToInt(Time.time % 60f);

        // mm:ss 형태의 문자열로 포맷팅합니다.
        timeText.text  = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowSetting() {
        if(settingPanel.activeSelf) {
            settingPanel.SetActive(false);
        } else {
            settingPanel.SetActive(true);
        }
    }

    public void UpdateGold(int goldCount) {
        gold.text = goldCount.ToString();
    }

    public void UpdateLife(string lifeStatus) {
        life.text = lifeStatus;
    }

    public void UnitSpawn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateWave(int wave) {
        if(wave == 0) checkWave[wave].SetActive(true);
        else
            if(wave%5 == 0) {
                for(int i=0; i<checkWave.Length; i++) {
                    checkWave[i].SetActive(false);
                }
            }
            // Wave Status 초기화
            checkWave[wave%5].SetActive(true);
    }

    //남아있는 몬스터 수 표시
    public void UpdateEnemyCount(int enemyCount) {
        enemyCntText.text = enemyCount.ToString();
    }

    public void StartGame() {
        information.SetActive(true);
        // informationText.gameObject.SetActive(true);
        informationText.text = "Monster Spawn Soon!";
        
    }

    public void StartSpawn() {
        information.SetActive(false);
        //informationText.enabled = false;
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

    public void ShowUnitStatus(UnitStats unitStats)
    {   
        nameText.text = unitStats.Name.ToString();
        gradeText.text = unitStats.Grade.ToString();
        damageText.text = unitStats.Damage.ToString();
    }

    public void RemoveUnitStatus() {
        nameText.text = "";
        gradeText.text = "";
        damageText.text = "";
    }

}
