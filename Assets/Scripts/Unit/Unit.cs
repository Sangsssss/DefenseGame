using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    private UnitMovement unitMovement;
    public UnitMovement UnitMovement { get {return unitMovement;} set {unitMovement = value;} }

    private UnitAttack unitAttack;
    public UnitAttack UnitAttack {  get {return unitAttack;} set {unitAttack = value;} }
  
    private UnitStats unitStats;
    public UnitStats UnitStats {  get {return unitStats;} set {unitStats = value;} }

    public GameObject selectCircle;

    public Action OnSell;
    
    void Awake()
    {
        unitMovement = this.GetComponent<UnitMovement>();
        unitAttack = this.GetComponent<UnitAttack>();
        unitStats = this.GetComponent<UnitStats>();
    }

    public void SelectUnit() {
        Debug.Log("Select Unit");
        selectCircle.SetActive(true);
        //Debug.Log("GRADE : " + unitStats.Grade);
    }

    public void DeselectUnit() {
        UIManager.instance.RemoveUnitStatus();
        selectCircle.SetActive(false);
    }

    public void Sell() {
        OnSell?.Invoke();
    }
}
