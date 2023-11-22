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
    public ParticleSystem spawnParticle;

    public Action OnSell;

    void Start() {
        StartCoroutine(ShowSpawnParticle());
    }
    
    void Awake()
    {
        unitMovement = this.GetComponent<UnitMovement>();
        unitAttack = this.GetComponent<UnitAttack>();
        unitStats = this.GetComponent<UnitStats>();
    }

    private IEnumerator ShowSpawnParticle()
    {
        spawnParticle.Play(); // 파티클 시스템 실행
        yield return new WaitForSeconds(1f); // 0.3초 동안 대기
        Destroy(spawnParticle.gameObject);
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
