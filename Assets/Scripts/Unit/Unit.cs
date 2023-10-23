using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    private UnitMovement unitMovement;

    private UnitAttack unitAttack;
  
    private UnitStats unitStats;

    

    public Action OnSell;
    
    void Start()
    {
        unitMovement = this.GetComponent<UnitMovement>();
        unitAttack = this.GetComponent<UnitAttack>();
        unitStats = this.GetComponent<UnitStats>();
    }

    public void Sell() {
        OnSell?.Invoke();
    }
}
