using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{	
	[SerializeField] Enums.BTNType btnType;
	private Transform buttonScale;
	private Vector3 defaultScale;

	void Awake() {
		buttonScale = this.GetComponent<Transform>();
	}

	void Start() {
		defaultScale = buttonScale.localScale;
	}

	public void Onclick()
	{	
		switch(btnType) {
			case Enums.BTNType.UPGRADE:
				break;
			case Enums.BTNType.NEW:
				break;
		}
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.05f;
    }
	public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
