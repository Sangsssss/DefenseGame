using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{   

    private RTSUnitController rtsUnitController;
    private UnitUpgrade unitUpgrade;
    [SerializeField] private CardManager cardManager;

    [Header ("Button Matching")]
    [SerializeField] private Button upgradeFire;
    [SerializeField] private Button upgradeIce;
    [SerializeField] private Button upgradeLight;
    [SerializeField] private Button upgradeDarkness;

    private bool sellMode;
    private bool multiSelectMode;
    private bool attackMode;
   
    // Start is called before the first frame update
    private void Awake() {
         rtsUnitController = this.GetComponent<RTSUnitController>();
         unitUpgrade = this.GetComponent<UnitUpgrade>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Escape)) {UIManager.instance.ShowSetting();}
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     button.image.color = clickedColor;
        // }
        // else if (Input.GetKeyUp(KeyCode.Mouse0))
        // {
        //     button.image.color = originalColor;
        // }
        if(Input.GetKeyDown(KeyCode.Alpha1)) { upgradeFire.onClick.Invoke(); }
        if(Input.GetKeyDown(KeyCode.Alpha2)) { upgradeIce.onClick.Invoke();}
        if(Input.GetKeyDown(KeyCode.Alpha3)) { upgradeLight.onClick.Invoke();}
        if(Input.GetKeyDown(KeyCode.Alpha4)) { upgradeDarkness.onClick.Invoke();}


        if(Input.GetKey(KeyCode.H)) { rtsUnitController.FreezeSelected(); }

        // R키를 누르면 카드 드로우
        if(Input.GetKeyDown(KeyCode.R)) { cardManager.DrawSpawnCards(); }

         if(Input.GetKey(KeyCode.A)) {
            attackMode = true;
        } else {
            attackMode = false;
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

     public bool IsAttackMode() {
        return attackMode;
    }
}
