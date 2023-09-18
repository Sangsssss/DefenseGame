using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{   

    [SerializeField]
    private UnitSpawner unitSpawner;
    // Start is called before the first frame update

    //Spawn Card 
    public List<GameObject> cardPrefabs; // 카드 이미지들을 저장할 리스트 = Ex. 4
    public Sprite cardShirt;
    public Image[] cardImages; // 카드 이미지를 표시할 UI 이미지들

    //Reward Card
    public List<GameObject> rewardPrefabs;
    public Image[] rewardImages;

    public Button drawButton; // 카드를 뽑는 버튼   
    private List<GameObject> selectedCards = new List<GameObject>(); // 선택된 카드들을 저장할 리스트


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
        for(int i = 0; i < cardImages.Length; i++) {
            int cardIndex = i; // Closer 이슈 ==> i 값을 고정
            cardImages[i].GetComponent<Button>().onClick.AddListener(() => SpawnUnit(cardIndex));
        }
        // 카드를 누르면 보상을 획득할 수 있게 버튼 연동
        for(int i = 0; i < rewardImages.Length; i++) {
            int rewardIndex = i; // Closer 이슈 ==> i 값을 고정
            rewardImages[i].GetComponent<Button>().onClick.AddListener(() => SelectRewards(rewardIndex));
        }

        // ?? 
        DrawCards();
    }
    

    // 카드 리셋
    public void DrawCards() {
        // 카드 섞는 소리 재생
        
        GameManager.instance.ShuffleCard();
        selectedCards.Clear();
        for (int i = 0; i < 4; i++)
        {   
            // 랜덤하게 카드 선택
            GameObject randomCard = cardPrefabs[Random.Range(0, cardPrefabs.Count)];
            selectedCards.Add(randomCard);
        }
        // 선택된 카드 이미지를 UI에 표시
        for (int i = 0; i < cardImages.Length; i++)
        {
            if (i < selectedCards.Count)
            {   
                cardImages[i].sprite = selectedCards[i].GetComponent<Image>().sprite;
                cardImages[i].gameObject.SetActive(true);
                cardImages[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                cardImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void SelectRewards(int rewardIndex) {
        //rewardImages[rewardIndex].GetComponent<CardAttribute>().cardType
    }

    public void DrawRewards() {
        UIManager.instance.drawRewardPanel();
        // 프리팹 중 카드 3장을 랜덤으로 배치한다.
    }


    // unitSpawner를 참조해서 유닛을 스폰하는게 옳은 방법일까??
    public void SpawnUnit(int index) {

         if(unitSpawner.CreateUnit(selectedCards[index].GetComponent<CardAttribute>().cardType)) {
            // 카드 뒤집기 
            cardImages[index].sprite = cardShirt;
         // 버튼 못 누르게 하기...
            cardImages[index].GetComponent<Button>().interactable = false;
         }
         
    }

}
