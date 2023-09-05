using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public FireUnitStats fireUnitStats;
    public IceUnitStats iceUnitStats;
    public LightUnitStats lightUnitStats;
    public DarknessUnitStats darknessUnitStats;


    public void UpgradeUnits(int index) {
        if(index == 0) {
            fireUnitStats.UpgradeDamage();
            Debug.Log("Completed Upgrade : " + fireUnitStats.Damage);
        }
        else if(index == 1) {
            iceUnitStats.UpgradeDamage();
            Debug.Log("Completed Upgrade : " + iceUnitStats.Damage);
        }
        else if(index == 2) {
            lightUnitStats.UpgradeDamage();
            Debug.Log("Completed Upgrade : " + lightUnitStats.Damage);
        } 
        else {
            darknessUnitStats.UpgradeDamage();
            Debug.Log("Completed Upgrade : " + darknessUnitStats.Damage);
        }  

    }

     
}
