using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<Sprite> cardSprites; // 카드 이미지들을 저장할 리스트 = Ex. 4
    public Image[] cardImages; // 카드 이미지를 표시할 UI 이미지들

    private List<Sprite> selectedCards = new List<Sprite>(); // 선택된 카드들을 저장할 리스트

    void Start()
    {
        
    }

    private void drawCards() {
        selectedCards.Clear();
        for (int i = 0; i < 4; i++)
        {
            // 랜덤하게 카드 선택
            Sprite randomCard = cardSprites[Random.Range(0, cardSprites.Count)];
            selectedCards.Add(randomCard);
        }
        // 선택된 카드 이미지를 UI에 표시
        for (int i = 0; i < cardImages.Length; i++)
        {
            if (i < selectedCards.Count)
            {
                cardImages[i].sprite = selectedCards[i];
                cardImages[i].gameObject.SetActive(true);
            }
            else
            {
                cardImages[i].gameObject.SetActive(false);
            }
        }
    }
}
