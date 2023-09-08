using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private RTSUnitController rtsUnitController;
    [SerializeField]
    private CardManager cardManager;

    private bool sellMode;
    private bool multiSelectMode;
   
    // Start is called before the first frame update
    private void Awake() {
         rtsUnitController = this.GetComponent<RTSUnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.H)) {
            rtsUnitController.FreezeSelected();
        }
        if(Input.GetKey(KeyCode.A)) {
            //rtsUnitController.AttackMonster();
        }
        // R키를 누르면 카드 드로우
        if(Input.GetKeyDown(KeyCode.R)) {
            cardManager.DrawCards();
        }
        // S키를 누르고 있을 때 Sell 모드로 변화
        if(Input.GetKey(KeyCode.S)) {
            sellMode = true;
        } else {
            sellMode = false;
        }
        // Shift키를 누르고 있을 때 Multi_Select모드로 변화
         if(Input.GetKey(KeyCode.LeftShift)) {
            multiSelectMode = true;
        } else {
            multiSelectMode = false;
        }
    }

    public bool IsSellMode() {
        return sellMode;
    }

    public bool IsMultiSelectMode() {
        return multiSelectMode;
    }
}
