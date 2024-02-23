using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{	
	[SerializeField] Enums.BTNType btnType;
	private Button customButton;
	private Transform buttonScale;
	private Vector3 defaultScale;


	void Awake() {
		buttonScale = this.GetComponent<Transform>();
		customButton =  this.GetComponent<Button>();
	}

	void Start() {
		defaultScale = buttonScale.localScale;
		customButton.onClick.AddListener(OnClick);
	}

	public void OnClick()
	{	
		switch(btnType) {
			case Enums.BTNType.UPGRADE:
				UpgradeUnit();
				break;
			case Enums.BTNType.NEW:
				break;
			case Enums.BTNType.SlIDE_CARD:
				break;
		}
	}
	private void UpgradeUnit() {
		GameManager.instance.PlayUpgradeSound();
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
