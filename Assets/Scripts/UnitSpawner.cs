using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;

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

    public void CreateUnit() {
        // 1. 골드가 없을 시
        if(GameManager.instance.goldCount < 2) {
            UIManager.instance.LackOfGold();
            return;
            
        }

        // 2. 골드 충분 ==> 유닛 생성 후 골드 감소
        Vector3 position = new Vector3(Random.Range(spawnPostionMin.x, spawnPostionMax.x), 0f, Random.Range(spawnPostionMin.y, spawnPostionMax.y));
        GameObject unitPrefab = RandomUnit();
        GameObject newUnit = Instantiate(unitPrefab, position, Quaternion.identity);

        GameManager.instance.UseGold();
        

        if(newUnit != null) {
            rtsUnitController.AddUnitToList(newUnit);
        }

        Debug.Log("유닛생성!");
    }

    // 만약 유닛마다 unitStats 스크립트를 가지고 있다면?? 
    // => Prefab마다 stat을 가짐 = SetUp 불필요, 
    private GameObject RandomUnit() {
        int randomInt = Random.Range(0, 101);
        if(randomInt > 0 && randomInt <= 25) {
            return unitPrefab[0]; // => fire ======. fire1, fire2, fire3, fire4, fire5 
        }
        else if(randomInt > 25 && randomInt <= 50) {
            return unitPrefab[1]; // => ice
        } 
          else if(randomInt > 50 && randomInt <= 75) {
            return unitPrefab[2]; // => light
        } 
        else {
            return unitPrefab[3]; // => darkness
        }
    }
}
