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
    private void Awake()
    {
        mainCamera = Camera.main;
        rtsUnitController = this.GetComponent<RTSUnitController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit)) {
                if(hit.transform.GetComponent<UnitMovement>() == null) return;
                
                if(Input.GetKey(KeyCode.LeftShift)) {
                    rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<UnitMovement>());
                } else {
                    rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<UnitMovement>());
                }
            } else {
                if(!Input.GetKey(KeyCode.LeftShift)) {
                    rtsUnitController.DeselectAll();
            }
        }

    }
    if(Input.GetMouseButtonDown(1)) {
            // 화면의 특정 지점에서 ray를 쏜다 = 마우스가 클릭한 지점에서
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerGrond)) {
                rtsUnitController.MoveSelected(hit.point);
            }
        }

    
       
    }
}
