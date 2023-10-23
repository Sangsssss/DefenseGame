using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
	private Animator animator;

	void Awake() {
		animator = this.GetComponent<Animator>();
	}

	void Start() {
		this.GetComponent<Button>().onClick.AddListener(() => Onclick());
	}
    public void Onclick()
	{
		animator.SetTrigger("Click");
	}
}
