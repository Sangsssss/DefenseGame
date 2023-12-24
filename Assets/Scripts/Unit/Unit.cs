using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Enums;

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
    public GameObject[] spawnParticles;

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
        GameObject magicCircle = GetParticle(unitStats.EUnitAttribute);
        magicCircle.GetComponent<ParticleSystem>().Play();
       // magicCircle.Play(); // 파티클 시스템 실행
        yield return new WaitForSeconds(2f); // 0.3초 동안 대기
        DestroyImmediate(magicCircle, true);
    }

    private GameObject GetParticle(EUnitAttribute eUnitAttribute)
    {   
        GameObject magicCircle = null;
        switch(eUnitAttribute) {
            case EUnitAttribute.FIRE :
                 magicCircle = Instantiate(spawnParticles[0], transform.position, Quaternion.identity);
                break;
            case EUnitAttribute.ICE :
                magicCircle = Instantiate(spawnParticles[1], transform.position, Quaternion.identity);
                break;
            case EUnitAttribute.LIGHT :
                 magicCircle = Instantiate(spawnParticles[2], transform.position, Quaternion.identity);
                break;
            case EUnitAttribute.DARKNESS :
                 magicCircle = Instantiate(spawnParticles[3], transform.position, Quaternion.identity);
                break;
            default :
                break;
        }
        return magicCircle;
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

    public void Spawn()
    {
    
    }
}
