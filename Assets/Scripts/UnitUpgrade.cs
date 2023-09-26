using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    private UnitStats[] unitStats = new UnitStats[4];
    private RTSUnitController rtsUnitController;


    // 업그레이드 단계마다 사용하는 골드 다르게 해야함.
    private int upgradeGold = 0;


    void Awake()
    {
        unitStats[0] = GameManager.instance.GetComponent<FireUnitStats>();
        unitStats[1] = GameManager.instance.GetComponent<IceUnitStats>();
        unitStats[2] = GameManager.instance.GetComponent<LightUnitStats>();
        unitStats[3] = GameManager.instance.GetComponent<DarknessUnitStats>();
        rtsUnitController = this.GetComponent<RTSUnitController>();
    }

    // 버튼 입력에 따라, Game Manager의 유닛 자체 스탯 향상 & Unit Controller에 의해 제어 중인 유닛 강화    
    public void UpgradeUnitsStat(int index)
    {
        if (index >= 0 && index < unitStats.Length && unitStats[index] != null)
        {
            // 돈이 부족하면 안됨
            if (GameManager.instance.goldCount < upgradeGold)
            {
                return;
            }
            else
            {
                unitStats[index].UpgradeDamage(); // 해당 인덱스의 유닛 스탯 업그레이드 => 앞으로 생성될 유닛에도 영향
                float upgradedDamage = unitStats[index].Damage; // 업그레이드된 데미지 가져오기
                rtsUnitController.UpgradeUnits(index, upgradedDamage); // 업그레이드 정보를 컨트롤러에 전달
                GameManager.instance.UpdateUpgrade(index); // 업그레이드 단계를 최신화
                Debug.Log("Completed Upgrade : " + upgradedDamage);
            }
        }
        else
        {
            Debug.LogWarning("Invalid index " + index);
        }

    }
    public void UpgradeUnit() {
        
    }


}
