using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
	private Animator animator;
	private Button button;
	private Color originalColor;
	private Color clickedColor;

	void Awake() {
		animator = this.GetComponent<Animator>();
		button = this.GetComponent<Button>();
	}

	void Start() {
		 button.onClick.AddListener(() => Onclick());
		originalColor = button.image.color;
        // 원래 색상의 80%로 새로운 색상
        clickedColor = new Color(100, 100, 100);
	}
    // public void OnPointerDown(PointerEventData eventData)
	// {
	// 	animator.SetBool("Click", true);
	// 	button.image.color = clickedColor;
	// }
	public void Onclick()
	{
		animator.SetBool("Click", true);
		button.image.color = clickedColor;
	}

	public void OnPointerUp(PointerEventData eventData)
    {	
		animator.SetBool("Click", false);
        button.image.color = originalColor;
    }
}
