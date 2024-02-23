using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Enums;

public class RTSUnitControllerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spot;

    [SerializeField]
    public List<Unit> AllUnits { private set; get;}
    public List<UnitStats> FireUnits { private set; get;}
    public List<UnitStats> IceUnits { private set; get;}
    public List<UnitStats> LightUnits { private set; get;}
    public List<UnitStats> DarknessUnits { private set; get;}
    private List<Unit> selectedUnitList;
    public Unit unit;
    

    void Awake()
    {
        selectedUnitList = new List<Unit>();
        AllUnits = new List<Unit>();
        FireUnits = new List<UnitStats>();
        IceUnits = new List<UnitStats>();
        LightUnits = new List<UnitStats>();
        DarknessUnits = new List<UnitStats>();
    }

    void Start() {
        Vector3 position = new Vector3(0,0,0);
        Unit newUnit = Instantiate(unit, position, Quaternion.identity);
        if (newUnit != null)
        {   
            Debug.Log("유닛 생성");
            AddUnitToList(newUnit);
        }
    }

    // linked each type buttons by index
    public void UpgradeUnits(EUnitAttribute eUnitAttribute) {  
        List<UnitStats> targetUnits = null;
        switch(eUnitAttribute) {
            case EUnitAttribute.FIRE :
                targetUnits = FireUnits;
                break;
            case EUnitAttribute.ICE :
                targetUnits = IceUnits;
                break;
            case EUnitAttribute.LIGHT :
                targetUnits = LightUnits;
                break;
            case EUnitAttribute.DARKNESS :
                targetUnits = DarknessUnits;
                break;
        }
        if(targetUnits != null) {
            for(int i = 0; i<targetUnits.Count; i++) {
                targetUnits[i].UpgradeDamage();
            }
        }
    }

    public void UpUnitSpeed() {
        for(int i = 0; i<AllUnits.Count; i++) {
            AllUnits[i].UnitStats.UpAttackSpeed();
            AllUnits[i].UnitMovement.UpMoveSpeed();
        }
    }
    
    public void DownUnitSpeed() {
        for(int i = 0; i<AllUnits.Count; i++) {
            AllUnits[i].UnitStats.DownAttackSpeed();
            AllUnits[i].UnitMovement.DownMoveSpeed();
        }
    }


    public void AddUnitToList(Unit newUnit)
    {   
        //UnitMovement unitMovement = newUnit.GetComponent<UnitMovement>();
        // UnitStats unitStats = newUnit.GetComponent<UnitStats>();
        // EUnitAttribute eUnitAttribute = unitStats.EUnitAttribute;

        AllUnits.Add(newUnit);
        
    }

    public void MoveSelected(Vector3 Destination) {
        spot.transform.position = new Vector3(Destination.x, spot.transform.position.y, Destination.z);
        spot.SetActive(true);
        StartCoroutine(CheckStop(Destination));
    }

    private IEnumerator CheckStop(Vector3 Destination) {
        // 코루틴 시작 시
        for(int i = 0; i < selectedUnitList.Count; i++) {
            selectedUnitList[i].UnitMovement.Move(Destination);
        }
        
        bool allUnitsMove = true;
        // 모든 유닛이 멈출 때까지 반복
        while(allUnitsMove) {
            allUnitsMove = false;
            for(int i = 0; i < selectedUnitList.Count; i++) {
                if(selectedUnitList[i].UnitMovement.isMoving == true) {
                    allUnitsMove = true;
                    break;
                }
            }
            yield return null;
        }
        spot.SetActive(false);
    }

    public void FreezeSelected() {
        for(int i = 0; i < selectedUnitList.Count; i++) {
            selectedUnitList[i].UnitMovement.Freeze();
        }
        
    }
      

    public void DeselectAll() {
        for(int i =0; i < selectedUnitList.Count; i++) {
           selectedUnitList[i].DeselectUnit();
        }
        selectedUnitList.Clear();
    }
    public void ClickSelectUnit(Unit newUnit) {
        DeselectAll();
        SelectUnit(newUnit);
    }

    public void ShiftClickSelectUnit(Unit newUnit) {
        if(selectedUnitList.Contains(newUnit)) {
            DeselectUnit(newUnit);
        } else {
            SelectUnit(newUnit);
        }
    }

    public void DragSelectUnit(Unit newUnit) {
        if(!selectedUnitList.Contains(newUnit)) { 
            SelectUnit(newUnit);
        }
    }

    private void SelectUnit(Unit newUnit) {
        newUnit.SelectUnit();
        selectedUnitList.Add(newUnit);
        // UnitStauts
        // if(selectedUnitList.Count <= 1) UIManager.instance.ShowUnitStatus(newUnit.UnitStats);
        // else UIManager.instance.RemoveUnitStatus(); 
    }

    private void DeselectUnit(Unit newUnit) {
        newUnit.DeselectUnit();
        selectedUnitList.Remove(newUnit);
    }

    public void SellUnit(Unit targetUnit) {
        // 마우스 커서를 클릭했을 때, 커서에 위치한 유닛을 판매 
        // if(targetUnit != null) {
        //     targetUnit.Sell();
        // }
        // targetUnit.Destroy();
        AllUnits.Remove(targetUnit);
        selectedUnitList.Remove(targetUnit);
        targetUnit.GetComponent<Unit>().Sell();
    }

    public void Attack()
    {   
        foreach (Unit unit in selectedUnitList) {
            // unit.GetComponent<UnitAttack>().Attack();
        }   

    }
}
