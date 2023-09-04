using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{   

    [SerializeField]
    private UnitSpawner unitSpawner;
    // Start is called before the first frame update
    public List<GameObject> cardPrefabs; // 카드 이미지들을 저장할 리스트 = Ex. 4
    public Image[] cardImages; // 카드 이미지를 표시할 UI 이미지들
    public Button drawButton; // 카드를 뽑는 버튼   
    private List<GameObject> selectedCards = new List<GameObject>(); // 선택된 카드들을 저장할 리스트

    void Start()
    {   
        // 버튼에 이벤트 추가 ( == 카드 드로우)
        drawButton.onClick.AddListener(DrawCards);
        for(int i = 0; i < cardImages.Length; i++) {
            int cardIndex = i; // Closer 이슈 ==> i 값을 고정
            cardImages[i].GetComponent<Button>().onClick.AddListener(() => SpawnUnit(cardIndex));
        }
    }
    

    // 카드 리셋
    private void DrawCards() {
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
            }
            else
            {
                cardImages[i].gameObject.SetActive(false);
            }
        }
    }


    // unitSpawner를 참조해서 유닛을 스폰하는게 옳은 방법일까??
    public void SpawnUnit(int index) {
         unitSpawner.CreateUnit(selectedCards[index].GetComponent<CardAttribute>().cardType);
    }
}