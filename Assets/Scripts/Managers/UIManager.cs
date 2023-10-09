using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using System.Transactions;
using UnityEditor.UIElements;
using System;
using System.Linq;

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
    [SerializeField] private Text enemyCntText;


    [Header ("Unit Count")]
    [SerializeField] private Text fireUnitCount;
    [SerializeField] private Text IceUnitCount;
    [SerializeField] private Text LightUnitCount;
    [SerializeField] private Text DarknessUnitCount;

    [Header ("Wave Status")]
    [SerializeField] private Image[] waveStatus; 
    [SerializeField] private Sprite checkWave;

    [Header ("Reward")]
    [SerializeField] private Image rewardPanel;

    [Header ("Upgrade")]
    [SerializeField] private Text[] upgradeStep;

    [Header ("Ohter")]
    public Text informationText;
    public Text gold;
    public Text life;
    
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
        waveStatus[(wave%6)-1].sprite = checkWave;
        waveText.text = "WAVE : " + wave;
    }
    
    //남아있는 몬스터 수 표시
    public void UpdateEnemyCount(int enemyCount) {
        enemyCntText.text = "남은 몬스터 수 : " + enemyCount;
    }

    public void StartGame() {
        informationText.text = "몬스터 스폰이 곧 시작됩니다!";
        informationText.enabled = true;
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
        informationText.text = "유닛 소환에 필요한 골드가 부족합니다.";
        informationText.enabled = true;
        yield return new WaitForSeconds(1.5f);
        informationText.enabled = false;
    }


    public void UpdateUnitStatus(UnitStats.UnitType type, int unitCount) {
        if(type == UnitStats.UnitType.Fire) {
            fireUnitCount.text = unitCount.ToString();
        } else if(type == UnitStats.UnitType.Ice) {
            IceUnitCount.text = unitCount.ToString();
        } else if(type == UnitStats.UnitType.Light) {
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

    internal void UpdateUnitStatus(UnitStats.UnitType type)
    {
        throw new NotImplementedException();
    }
}
