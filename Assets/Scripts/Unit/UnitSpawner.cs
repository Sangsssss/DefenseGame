using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Enums;

public class UnitSpawner : MonoBehaviour
{   
    private Vector2 spawnPostionMin = new Vector2(-2, -2);
    private Vector2 spawnPostionMax = new Vector2(2, 2);


    [Header ("Unit Category")]
    [SerializeField] private List<Unit> FireUnitPrefab;
    [SerializeField] private List<Unit> IceUnitPrefab;
    [SerializeField] private List<Unit> LightUnitPrefab;
    [SerializeField] private List<Unit> DarknessUnitPrefab;

    private RTSUnitController rtsUnitController;
    private UnitUpgrade unitUpgrade;
    private EUnitAttribute atttributeType;
    [SerializeField] private GameObject[] magicCircle;


    // Start is called before the first frame update
    void Awake()
    {
        rtsUnitController = this.GetComponent<RTSUnitController>();
        unitUpgrade = this.GetComponent<UnitUpgrade>();
    }

    // Gold 2 소비하며, 유닛 생성


    // 보상 시
    public void RewardUnit(int grade) {
        float randNum = Random.Range(0, 100);
        if(randNum >= 75) {
            atttributeType = EUnitAttribute.FIRE;
        } else if(randNum >= 50) {
            atttributeType = EUnitAttribute.ICE;
        } else if(randNum >= 25) {
            atttributeType = EUnitAttribute.LIGHT;
        } else {
            atttributeType = EUnitAttribute.DARKNESS;
        }
        SpawnUnit(atttributeType, grade);
    }

    // 스폰 시
    public void SpawnUnit(EUnitAttribute attributeType, int grade)
    {
        Vector3 position = new Vector3(Random.Range(spawnPostionMin.x, spawnPostionMax.x), 0f, Random.Range(spawnPostionMin.y, spawnPostionMax.y));

        Unit unitPrefab = LinkedUnit(attributeType, grade);
        Unit newUnit = Instantiate(unitPrefab, position, Quaternion.identity);

        // 유닛 스탯 스크립트에서 타입을 확인하기 위함
        UnitStats unitStats = newUnit.GetComponent<UnitStats>();

        if (unitStats != null)
        {   
            unitStats.SetUpUnitStat(unitUpgrade.GetUnitStats(attributeType, grade));
        }

        newUnit.OnSell += () =>
        {
            Destroy(newUnit.gameObject);
            GameManager.instance.GainGold(2);
        };

        if (newUnit != null)
        {
            rtsUnitController.AddUnitToList(newUnit);
        }
    }

    // 만약 유닛마다 unitStats 스크립트를 가지고 있다면?? 
    // => Prefab마다 stat을 가짐 = SetUp 불필요, 
    private Unit LinkedUnit(EUnitAttribute attributeType, int grade) {
        if(attributeType == EUnitAttribute.FIRE) {
            return FireUnitPrefab[grade-1];
        }
        else if(attributeType == EUnitAttribute.ICE) {
            return IceUnitPrefab[grade-1];
        }
        else if(attributeType == EUnitAttribute.LIGHT) {
            return LightUnitPrefab[grade-1];
        } 
        else if(attributeType == EUnitAttribute.DARKNESS){
            return DarknessUnitPrefab[grade-1];
        } else {
            return null;
        }
    }
}
