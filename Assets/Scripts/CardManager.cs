using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> cardPrefabs; // 카드 이미지들을 저장할 리스트 = Ex. 4
    public Image[] cardImages; // 카드 이미지를 표시할 UI 이미지들
    public Button drawButton; // 카드를 뽑는 버튼   
    private List<GameObject> selectedCards = new List<GameObject>(); // 선택된 카드들을 저장할 리스트

    void Start()
    {
        drawButton.onClick.AddListener(DrawCards);
        for(int i = 0; i < cardImages.Length; i++) {
            cardImages[i].GetComponent<Button>().onClick.AddListener(() => SpawnUnit(i));
        }
    }
  
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

    private void SpawnUnit(int index) {
        
    }
}
