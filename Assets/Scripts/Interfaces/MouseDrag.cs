using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    [SerializeField]
    private RectTransform dragRectangle; // 드래그 범위 가시화 Image UI

    private Rect dragRect; // 드래그 범위
    private Vector2 start = Vector2.zero; // 드래그 시작
    private Vector2 end = Vector2.zero; // 드래그 종료

    private Camera mainCamera;
    private RTSUnitController rtsUnitController;

    // Start is called before the first frame update
    void Awake()
    {   
        mainCamera = Camera.main;
        rtsUnitController = GetComponent<RTSUnitController>();
        DrawDragRectagle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            start = Input.mousePosition;
            dragRect = new Rect();
        }
        if(Input.GetMouseButton(0)) {
            end = Input.mousePosition;
            DrawDragRectagle();
        }
        if(Input.GetMouseButtonUp(0)) {
            CalculateDragRect();
            SelectUnits();
            
            start = end = Vector2.zero;
            DrawDragRectagle();
        }
    }

    private void SelectUnits()
    {   
        // 모든 유닛을 검사
        foreach(Unit unit in rtsUnitController.AllUnits) {
            // 유닛의 월드 좌표를 화면 좌표로 변환 => 드래그 범위 내 있는지 검사
            if(dragRect.Contains(mainCamera.WorldToScreenPoint(unit.transform.position))) {
                rtsUnitController.DragSelectUnit(unit);
            } 
        }
    }

    private void CalculateDragRect()
    {
        if(Input.mousePosition.x < start.x) {
            dragRect.xMin = Input.mousePosition.x;
            dragRect.xMax = start.x;
        } else {
            dragRect.xMin = start.x;
            dragRect.xMax = Input.mousePosition.x;
        }

        if(Input.mousePosition.y < start.y) {
            dragRect.yMin = Input.mousePosition.y;
            dragRect.yMax = start.y;
        } else {
            dragRect.yMin = start.y;
            dragRect.yMax = Input.mousePosition.y;
        }
    }

    private void DrawDragRectagle()
    {
        dragRectangle.position = (start + end) * 0.5f;
        dragRectangle.sizeDelta = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));

    }

}
