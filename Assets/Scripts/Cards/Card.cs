using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    
    private Animator animator;

    public GameObject front;
    public GameObject back;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void OnPointerDown()
    {   
        Debug.Log("OnPointerDown");
        animator.SetTrigger("Flip");
    }

    public void ShowBack()
    {
        back.SetActive(true);
    }

    public void ShowFront() {
        back.SetActive(false);
    }

    public void ResetRotation() {
        animator.SetTrigger("BackFlip");
        ShowFront();
    }
}
