using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private LayerMask layerUnit;
    [SerializeField]
    private LayerMask layerGrond;

    private Camera mainCamera;
    private RTSUnitController rtsUnitController;
    private KeyboardInput keyboardInput;
    private void Awake()
    {
        mainCamera = Camera.main;
        rtsUnitController = this.GetComponent<RTSUnitController>();
        keyboardInput = this.GetComponent<KeyboardInput>();
    }

    // Update is called once per frame
    private void Update()
    {   
        // keyboard Input을 받아와, Sell Mode일 때와 이닐 때로 구분

        // 만약 shift와 s를 동시에 누르면 ?? 

        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit)) {
                // 마우스 커서에 위치하는 오브젝트가 null일 때,
                Unit targetUnit = hit.transform.GetComponent<Unit>();
                if(targetUnit == null) return;
                // Shift를 눌렀을 때와 아닐 때로 구분
                if(keyboardInput.IsSellMode() == true) {
                     rtsUnitController.SellUnit(targetUnit);
                     Debug.Log("Sell Unit");
                }
                else if(keyboardInput.IsMultiSelectMode() == true) {
                    rtsUnitController.ShiftClickSelectUnit(targetUnit);
                } else {
                    rtsUnitController.ClickSelectUnit(targetUnit);
                }
            } else {
                //UIManager.instance.ShowUnitStatus();
                if(keyboardInput.IsMultiSelectMode() == false) {  
                    rtsUnitController.DeselectAll();
            }
        }

    }
    if(Input.GetMouseButtonDown(1)) {
            // 화면의 특정 지점에서 ray를 쏜다 = 마우스가 클릭한 지점에서
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerGrond)) {
                Debug.Log(hit.point);
                rtsUnitController.MoveSelected(hit.point);
            }
        }

    
       
    }
}
