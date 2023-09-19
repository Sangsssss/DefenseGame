using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RTSUnitController : MonoBehaviour
{
    // Start is called before the first frame update
    public UnitSpawner unitSpanwer;
    public GameObject spot;

    [SerializeField]
    public List<UnitMovement> AllUnits { private set; get;}
    public List<UnitStats> FireUnits { private set; get;}
    public List<UnitStats> IceUnits { private set; get;}
    public List<UnitStats> LightUnits { private set; get;}
    public List<UnitStats> DarknessUnits { private set; get;}


    private List<UnitMovement> selectedUnitList;
    void Awake()
    {
        selectedUnitList = new List<UnitMovement>();
        AllUnits = new List<UnitMovement>();
        FireUnits = new List<UnitStats>();
        IceUnits = new List<UnitStats>();
        LightUnits = new List<UnitStats>();
        DarknessUnits = new List<UnitStats>();
    }

    void Start() {
        
    }

    // linked each type buttons by index
    public void UpgradeUnits(int index, float upgradeDamage) {  
        // Upgrade Fire Units 
        if(index == 0) {
            for(int i = 0; i<FireUnits.Count; i++) {
                FireUnits[i].GetComponent<UnitStats>().Damage = upgradeDamage;
            }
        }
        // Upgrade Ice Units 
        else if (index == 1) {
             for(int i = 0; i<IceUnits.Count; i++) {
                IceUnits[i].GetComponent<UnitStats>().Damage = upgradeDamage;
            }
        }
        // Upgrade Light Units
        else if (index == 2) {

        }
        // Upgrade Darkness Units 
        else {

        }
    }



    public void AddUnitToList(Unit newUnit)
    {   
        UnitMovement unitMovement = newUnit.GetComponent<UnitMovement>();
        UnitStats unitStats = newUnit.GetComponent<UnitStats>();

        AllUnits.Add(unitMovement);
        
        switch(unitStats.Type)
        {
            case UnitStats.UnitType.Fire:
                FireUnits.Add(unitStats);
                UIManager.instance.UpdateUnitStatus(unitStats.Type, FireUnits.Count);
                break;
            case UnitStats.UnitType.Ice:
                IceUnits.Add(unitStats);
                UIManager.instance.UpdateUnitStatus(unitStats.Type, IceUnits.Count);
                break;
            case UnitStats.UnitType.Light:
                LightUnits.Add(unitStats);
                UIManager.instance.UpdateUnitStatus(unitStats.Type, LightUnits.Count);
                break;
            case UnitStats.UnitType.Darkness:
                DarknessUnits.Add(unitStats);
                UIManager.instance.UpdateUnitStatus(unitStats.Type, DarknessUnits.Count);
                break;
        }
    }

    public void MoveSelected(Vector3 Destination) {
        spot.transform.position = new Vector3(Destination.x, spot.transform.position.y, Destination.z);
        spot.SetActive(true);
        StartCoroutine(CheckStop(Destination));
    }

    private IEnumerator CheckStop(Vector3 Destination) {
        // 코루틴 시작 시
        for(int i = 0; i < selectedUnitList.Count; i++) {
            selectedUnitList[i].Move(Destination);
        }
        
        bool allUnitsMove = true;
        // 모든 유닛이 멈출 때까지 반복
        while(allUnitsMove) {
            allUnitsMove = false;
            for(int i = 0; i < selectedUnitList.Count; i++) {
                if(selectedUnitList[i].isMoving == true) {
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
            selectedUnitList[i].Freeze();
        }
        
    }
      

    public void DeselectAll() {
        for(int i =0; i < selectedUnitList.Count; i++) {
            selectedUnitList[i].DeselectUnit();
        }
        selectedUnitList.Clear();
    }
    public void ClickSelectUnit(UnitMovement newUnit) {
        DeselectAll();
        SelectUnit(newUnit);
    }

    public void ShiftClickSelectUnit(UnitMovement newUnit) {
        if(selectedUnitList.Contains(newUnit)) {
            DeselectUnit(newUnit);
        } else {
            SelectUnit(newUnit);
        }
    }

    public void DragSelectUnit(UnitMovement newUnit) {
        if(!selectedUnitList.Contains(newUnit)) { 
            SelectUnit(newUnit);
        }
    }

    private void SelectUnit(UnitMovement newUnit) {
        newUnit.SelectUnit();
        selectedUnitList.Add(newUnit);
    }
    private void DeselectUnit(UnitMovement newUnit) {
        newUnit.DeselectUnit();
        selectedUnitList.Remove(newUnit);
    }

    public void SellUnit(UnitMovement targetUnit) {
        // 마우스 커서를 클릭했을 때, 커서에 위치한 유닛을 판매 
        // if(targetUnit != null) {
        //     targetUnit.Sell();
        // }
        // targetUnit.Destroy();
        AllUnits.Remove(targetUnit);
        selectedUnitList.Remove(targetUnit);
        targetUnit.GetComponent<Unit>().Sell();
    }
}
