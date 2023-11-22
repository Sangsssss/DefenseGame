using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class UnitUpgrade : MonoBehaviour
{   
    private Enums.SpendType spendType;
    private RTSUnitController rtsUnitController;
    [SerializeField] private UnitStatSO unitStatSO;
    private List<UnitStatData> fireUnitStats;
    private List<UnitStatData> iceUnitStats;
    private List<UnitStatData> lightUnitStats;
    private List<UnitStatData> darknessUnitStats;

       void Awake()
    {
        rtsUnitController = this.GetComponent<RTSUnitController>();
        fireUnitStats = new List<UnitStatData>(); 
        iceUnitStats = new List<UnitStatData>();
        lightUnitStats = new List<UnitStatData>(); 
        darknessUnitStats = new List<UnitStatData>();
    }


    private void Start()
    {
        // Scritable Object에 저장된 데이터를 속성별로 분류해서 저장
        UnitStatData[] unitData = unitStatSO.UnitStatData;
        UnitStatData[] copied = new UnitStatData[unitData.Length];

        // Scritable Object의 데이터를 참조하지 못하게 복사본 생성
        for(int i = 0; i < unitData.Length; i++) {
            copied[i] = new UnitStatData(unitData[i]);
        }
 
        fireUnitStats.AddRange(copied.Where(unit => unit.EUnitAttribute 
            == Enums.EUnitAttribute.FIRE).OrderBy(unit => unit.Grade));;
        iceUnitStats.AddRange(copied.Where(unit => unit.EUnitAttribute 
            == Enums.EUnitAttribute.ICE).OrderBy(unit => unit.Grade));
        lightUnitStats.AddRange(copied.Where(unit => unit.EUnitAttribute 
            == Enums.EUnitAttribute.LIGHT).OrderBy(unit => unit.Grade));
        darknessUnitStats.AddRange(copied.Where(unit => unit.EUnitAttribute 
            == Enums.EUnitAttribute.DARKNESS).OrderBy(unit => unit.Grade));
    }
    
 
    public UnitStatData GetUnitStats(Enums.EUnitAttribute eUnitAttribute, int grade) {
        UnitStatData newStats = null;
        switch(eUnitAttribute) {
            case Enums.EUnitAttribute.FIRE :
                newStats = fireUnitStats.ElementAtOrDefault(grade-1);
                break;
            case Enums.EUnitAttribute.ICE :
                newStats = iceUnitStats.ElementAtOrDefault(grade-1);
                break;
            case Enums.EUnitAttribute.LIGHT :
                newStats = lightUnitStats.ElementAtOrDefault(grade-1);
                break;
            case Enums.EUnitAttribute.DARKNESS :
                newStats = darknessUnitStats.ElementAtOrDefault(grade-1);
                break;
        }
        return newStats;
    }   

    public void UpgradeFire() {
        if(GameManager.instance.UseGold(Enums.SpendType.FIREUPGRADE)) {
            UpgradeUnit(Enums.EUnitAttribute.FIRE);
        }  
    }
    public void UpgradeIce() {
        if(GameManager.instance.UseGold(Enums.SpendType.ICEUPGRADE)) {
            UpgradeUnit(Enums.EUnitAttribute.ICE);
        }    
    }
    public void UpgradeLight() {
         if(GameManager.instance.UseGold(Enums.SpendType.LIGHTUPGRADE)) {
            UpgradeUnit(Enums.EUnitAttribute.LIGHT);
        }    
    }
    public void UpgradeDarkness() {
        if(GameManager.instance.UseGold(Enums.SpendType.DARKNESSUPGRADE)) {
            UpgradeUnit(Enums.EUnitAttribute.DARKNESS);
        }    
    }

    // 버튼 입력에 따라, Game Manager의 유닛 자체 스탯 향상 & Unit Controller에 의해 제어 중인 유닛 강화    

    public void RewardUnitsStat(int grade) {
        Enums.EUnitAttribute eUnitAttribute;
        float randNum = Random.Range(0, 100);
        if(randNum >= 75) {
            eUnitAttribute = Enums.EUnitAttribute.FIRE;
        } else if(randNum >= 50) {
            eUnitAttribute = Enums.EUnitAttribute.ICE;
        } else if(randNum >= 25) {
            eUnitAttribute = Enums.EUnitAttribute.LIGHT;
        } else {
            eUnitAttribute = Enums.EUnitAttribute.DARKNESS;
        }
        for(int i =0; i<grade; i++){
            UpgradeUnit(eUnitAttribute);
        }
    }


    // 해당 타입의 데이터에 접근 => 데미지를 업그레이드
    public void UpgradeUnit(Enums.EUnitAttribute eUnitAttribute) {
        List<UnitStatData> targetUnitStats = null;
        
        GameManager.instance.UpdateUpgrade((Enums.SpendType) eUnitAttribute);

        if (eUnitAttribute == Enums.EUnitAttribute.FIRE) {targetUnitStats = fireUnitStats;}
        else if (eUnitAttribute == Enums.EUnitAttribute.ICE) {targetUnitStats = iceUnitStats;}
        else if (eUnitAttribute == Enums.EUnitAttribute.LIGHT) {targetUnitStats = lightUnitStats;}
        else if (eUnitAttribute == Enums.EUnitAttribute.DARKNESS) {targetUnitStats = darknessUnitStats;}

        if (targetUnitStats != null)
        {
            foreach (var targetUnitStat in targetUnitStats) 
            {   
                // 유닛 스탯 데이터 업그레이드 
                targetUnitStat.UpgradeDamage();
                Debug.Log("Upgrade" + eUnitAttribute);
                // 유닛 자체를 업그레이드
                rtsUnitController.UpgradeUnits(eUnitAttribute);
            }
        }
        }
       
        // float upgradedDamage = unitStats[index].Damage; // 업그레이드된 데미지 가져오기
        // rtsUnitController.UpgradeUnits(index, upgradedDamage); // 업그레이드 정보를 컨트롤러에 전달
        // GameManager.instance.UpdateUpgrade(index); // 업그레이드 단계를 최신화
        // Debug.Log("Completed Upgrade : " + upgradedDamage);
    }



