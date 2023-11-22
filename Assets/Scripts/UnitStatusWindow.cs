using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitStatusWindow : MonoBehaviour
{   
    [SerializeField] private TMP_Text unitStatus;

    void Awake() {
        unitStatus = this.GetComponent<TMP_Text>();
    }

    public void SetUnitStatus(UnitStats unitStats, float x, float z)
    {   
        unitStatus.text = "N : " + "NAME" + "\nG : " + unitStats.Grade.ToString() + "\nD : " + unitStats.Damage.ToString();
    }

}
