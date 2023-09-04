using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using System.Transactions;

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

    public Button unitSpawnButton;  
    public Text waveText;
    public Text timeText;
    public Text enemyCntText;
    public Text informationText;
    public Text unitStatus;
    public Text gold;
    public Text life;
    [SerializeField]
    private CardManager cardPanel;
    
    // Start is called before the first frame update

    void Awake() {

    }


    void Start()
    {
        
    }

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


    public void UpdateUnitStatus(int unitsCount, int fire, int ice, int light, int darkness) {
        unitStatus.text = "Units : " + unitsCount + "\n" 
        + "Fire : " + fire + "\n"
        + "Ice : " + ice  + "\n"
        + "Light : " + light  + "\n"
        + "Darkness :" + darkness;
  
    }

    
}
