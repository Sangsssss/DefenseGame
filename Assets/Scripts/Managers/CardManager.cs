using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{   
    // [Header ["Component"] ]
    public Button drawButton; // 카드를 뽑는 버튼   
    
    [SerializeField] private UnitSpawner unitSpawner;
    [SerializeField] private UnitUpgrade unitUpgrade;
    // Start is called before the first frame update

    [SerializeField] private SpawnCardSO spawnCardSO;
    [SerializeField] private RewardCardSO rewardCardSO;
    [SerializeField] private List<SpawnCard> spawnCards;
    [SerializeField] private List<RewardCard> rewardCards;
    private List<SpawnCardData> spawnCardBuffer; 
    private List<RewardCardData> rewardCardBuffer;
    private bool isStart = true;


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
        drawButton.onClick.AddListener(DrawSpawnCards);
        
        // 카드를 누르면 유닛을 스폰할 수 있게 버튼 연동
        for(int i = 0; i < spawnCards.Count; i++) {
            int cardIndex = i; // Closer 이슈 ==> i 값을 고정
            spawnCards[i].GetComponent<Button>().onClick.AddListener(() => SpawnUnit(cardIndex));
        }
        //카드를 누르면 보상을 획득할 수 있게 버튼 연동
        for(int i = 0; i < rewardCards.Count; i++) {
            int rewardIndex = i; // Closer 이슈 ==> i 값을 고정
            rewardCards[i].GetComponent<Button>().onClick.AddListener(() => SelectRewards(rewardIndex));
        }

        DrawSpawnCards();
        // UIManager.instance.VisibleUI();
        isStart = false;
    }
    

    private void RandomSpawnCards() {
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

     private void RandomRewardCards() {
        // 카드 섞는 소리 재생
        // GameManager.instance.ShuffleCard();
        rewardCardBuffer = new List<RewardCardData>();
        // 0~100사이 랜덤 난수 생성
        // 알고리즘 만들어야함.
        for(int i = 0; i < rewardCards.Count; i++) {
            float randomNum = Random.Range(0, 100);
            if(randomNum <= 33) {
                rewardCardBuffer.Add(rewardCardSO.RewardCardData[0]);
            } else if(randomNum <= 66) {
                rewardCardBuffer.Add(rewardCardSO.RewardCardData[1]);
            } else {
                rewardCardBuffer.Add(rewardCardSO.RewardCardData[2]);
            }
        }
    }

    // 스폰 카드 리셋
    public void DrawSpawnCards() {
        // 처음 시작 시는 돈 소모 X
        if(!isStart) {
            if(!GameManager.instance.ShuffleCard()) return;
        }
        // 1. 카드를 섞는다
        RandomSpawnCards();
        // 카드 섞는 소리 재생 + 돈 소모
        // 2. 카드를 UI에 배치한다.
         for (int i = 0; i < spawnCards.Count; i++)
        {   
            if(!isStart) {
                spawnCards[i].ResetRotation();
            }
            spawnCards[i].SetUpCard(spawnCardBuffer[i]);
        }
    }

    // 리워드 카드 리셋
    public void DrawRewardCards() {
        // 1. 카드를 섞는다
        RandomRewardCards();
        // 2. 카드를 UI에 배치한다.
         for (int i = 0; i < rewardCards.Count; i++)
        {   
            rewardCards[i].ResetRotation();

            rewardCards[i].SetUpCard(rewardCardBuffer[i]);
            rewardCards[i].gameObject.SetActive(true); // ???
        }
         UIManager.instance.DrawRewardPanel();
    }

    public void SelectRewards(int rewardIndex) {
        RewardCard selectedRewardCard = rewardCards[rewardIndex];
        selectedRewardCard.OnPointerDown();
        switch(selectedRewardCard.RewardType) {
            case Enums.ERewardType.GOLD :
                // 카드에 해당하는 만큼으 골드 제공
                GameManager.instance.GainGold(selectedRewardCard.Grade * 10);
                break;
            case Enums.ERewardType.UNIT :
                // 카드에 해당하는 등급의 유닛을 제공
                unitSpawner.RewardUnit(selectedRewardCard.Grade);
                break;
            case Enums.ERewardType.STAT :
                // 카드에 해당하는 만큼 강화
                unitUpgrade.RewardUnitsStat(selectedRewardCard.Grade);
                break;    
        }
        UIManager.instance.RemoveRewardPanel();
    }
 

    // unitSpawner를 참조해서 유닛을 스폰하는게 옳은 방법일까??
    public void SpawnUnit(int index) {
               // 1. 골드가 없을 시
        if (GameManager.instance.goldCount < 2)
        {
            UIManager.instance.LackOfGold();            
        } else {
            spawnCards[index].OnPointerDown();
            unitSpawner.SpawnUnit(spawnCards[index].AttributeType, spawnCards[index].Grade);
        }
    }
}
