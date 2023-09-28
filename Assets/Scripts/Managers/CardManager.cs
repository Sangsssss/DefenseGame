using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{   

    [SerializeField]
    private UnitSpawner unitSpawner;
    [SerializeField]
    private UnitUpgrade unitUpgrade;
    // Start is called before the first frame update

    [SerializeField] private SpawnCardSO spawnCardSO;
    [SerializeField] private List<SpawnCard> spawnCards;
    private List<SpawnCardData> spawnCardBuffer; 

    public Sprite cardShirt;

    //Reward Card
    public List<GameObject> rewardPrefabs;
    public Image[] rewardImages;

    public Button drawButton; // 카드를 뽑는 버튼   
    private List<GameObject> selectedSpawnCards = new List<GameObject>(); // 선택된 카드들을 저장할 리스트
    private List<GameObject> selectedRewardCards = new List<GameObject>();

    private int spanwCardNum = 4;


    public static CardManager Instance {
        get {
            if(c_instance == null) {
                c_instance = FindObjectOfType<CardManager>();
            }
            return c_instance;
        }
    }

    // Start is called before the first frame update
    private static CardManager c_instance;


    void Awake() {

    }

    void Start()
    {   
        // 버튼에 이벤트 추가 ( == 카드 드로우)
        drawButton.onClick.AddListener(DrawCards);
        
        // 카드를 누르면 유닛을 스폰할 수 있게 버튼 연동
        for(int i = 0; i < spawnCards.Count; i++) {
            int cardIndex = i; // Closer 이슈 ==> i 값을 고정
            spawnCards[i].GetComponent<Button>().onClick.AddListener(() => SpawnUnit(cardIndex));
        }
        //카드를 누르면 보상을 획득할 수 있게 버튼 연동
        for(int i = 0; i < rewardImages.Length; i++) {
            int rewardIndex = i; // Closer 이슈 ==> i 값을 고정
            rewardImages[i].GetComponent<Button>().onClick.AddListener(() => SelectRewards(rewardIndex));
        }

        // ?? 
        DrawCards();
    }
    

    private void RandomSpawnCards() {
        // 카드 섞는 소리 재생
        GameManager.instance.ShuffleCard();
        spawnCardBuffer = new List<SpawnCardData>();
        // 0~100사이 랜덤 난수 생성
        // 알고리즘 만들어야함.
        for(int i = 0; i < spawnCards.Count; i++) {
            float randomNum = Random.Range(0, 100);
            if(randomNum >= 0) {
            spawnCardBuffer.Add(spawnCardSO.SpawnCardData[0]);
        }
        }
    }

    // 카드 리셋
    public void DrawCards() {
        // 1. 카드를 섞는다
        RandomSpawnCards();
        // 2. 카드를 UI에 배치한다.
         for (int i = 0; i < spawnCards.Count; i++)
        {   
            spawnCards[i].SetUpCard(spawnCardBuffer[i]);
            spawnCards[i].gameObject.SetActive(true); // ???
            spawnCards[i].GetComponent<Button>().interactable = true;
        }
    }

    public void SelectRewards(int rewardIndex) {
        RewardCard selectedReward = selectedRewardCards[rewardIndex].GetComponent<RewardCard>();
        int reward = selectedReward.RewardCardData.Reward;
        switch(selectedReward.RewardCardData.Type) {
            case RewardCardData.RewardType.GOLD :
                // 카드에 해당하는 만큼으 골드 제공
                GameManager.instance.GainGold(reward);
                break;
            case RewardCardData.RewardType.UNIT :
                // 카드에 해당하는 등급의 유닛을 제공
               // unitSpawner.SpawnUnit(selectedReward.Attribute, reward);
                break;
            case RewardCardData.RewardType.UPGRADE :
                // 카드에 해당하는 만큼 강화
               // unitUpgrade.UpgradeUnit(selectedReward.Attribute, reward);
                break;    
        }
        UIManager.instance.RemoveRewardPanel();
    }

    public void DrawRewards() {
        // 프리팹 중 카드 3장을 랜덤으로 배치한다.
        selectedRewardCards.Clear();
        for (int i = 0; i < 3; i++)
        {   
            // 랜덤하게 카드 선택
            GameObject randomReward= rewardPrefabs[Random.Range(0, rewardPrefabs.Count)];
            selectedRewardCards.Add(randomReward);
        }
        UIManager.instance.DrawRewardPanel();
    }


    // unitSpawner를 참조해서 유닛을 스폰하는게 옳은 방법일까??
    public void SpawnUnit(int index) {

         if(unitSpawner.CreateUnit(spawnCards[index].AttributeType)) {
            // 카드 뒤집기 
            spawnCards[index].GetComponent<Image>().sprite = cardShirt;
         // 버튼 못 누르게 하기...
            spawnCards[index].GetComponent<Button>().interactable = false;
         }
         
    }

}
