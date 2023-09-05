using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;
using static CardAttribute;

public class UnitSpawner : MonoBehaviour
{
    private Vector2 spawnPostionMin = new Vector2(-2, -2);
    private Vector2 spawnPostionMax = new Vector2(2, 2);

    public List<GameObject> unitPrefab; //fire, ice, light, darkness
    private RTSUnitController rtsUnitController;


    // Start is called before the first frame update
    void Awake()
    {
        rtsUnitController = this.GetComponent<RTSUnitController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Gold 2 소비하며, 유닛 생성

    public void CreateUnit(CardType cardType) {
        // 1. 골드가 없을 시
        if(GameManager.instance.goldCount < 2) {
            UIManager.instance.LackOfGold();
            return;
            
        }

        // 2. 골드 충분 ==> 유닛 생성 후 골드 감소
        Vector3 position = new Vector3(Random.Range(spawnPostionMin.x, spawnPostionMax.x), 0f, Random.Range(spawnPostionMin.y, spawnPostionMax.y));
        GameObject unitPrefab = LinkedUnit(cardType);
        GameObject newUnit = Instantiate(unitPrefab, position, Quaternion.identity);
        UnitStats unitStats = newUnit.GetComponent<UnitStats>();

        if(unitStats != null) {
            switch(unitStats.Type) {
                case UnitStats.UnitType.Fire: 
                    unitStats.Damage = GameManager.instance.GetComponent<FireUnitStats>().Damage;
                    break;
                case UnitStats.UnitType.Ice: 
                    unitStats.Damage = GameManager.instance.GetComponent<IceUnitStats>().Damage;
                    break;
                case UnitStats.UnitType.Light:  
                    unitStats.Damage = GameManager.instance.GetComponent<LightUnitStats>().Damage;
                    break;
                case UnitStats.UnitType.Darkness: 
                    unitStats.Damage = GameManager.instance.GetComponent<DarknessUnitStats>().Damage;
                    break;
                
            }    
        }

        GameManager.instance.UseGold();

        if(newUnit != null) {
            rtsUnitController.AddUnitToList(newUnit);
        }

        Debug.Log("유닛생성!");
    }

    // 만약 유닛마다 unitStats 스크립트를 가지고 있다면?? 
    // => Prefab마다 stat을 가짐 = SetUp 불필요, 
    private GameObject LinkedUnit(CardType cardType) {
        if(cardType == CardType.Fire) {
            return unitPrefab[0];
        }
        else if(cardType == CardType.Ice) {
            return unitPrefab[1];
        }
        else if(cardType == CardType.Light) {
            return unitPrefab[2];
        } 
        else {
            return unitPrefab[3];
        }  
    }
}
