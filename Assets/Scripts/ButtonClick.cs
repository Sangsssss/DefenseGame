using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        clickedColor = new Color(originalColor.r, originalColor.g * 0.6f, originalColor.b * 0.6f);
	}
    public void Onclick()
	{
		animator.SetTrigger("Click");
		button.image.color = clickedColor;
	}
}
