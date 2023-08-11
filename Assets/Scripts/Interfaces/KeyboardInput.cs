using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private RTSUnitController rtsUnitController;
   
    // Start is called before the first frame update
    private void Awake() {
         rtsUnitController = this.GetComponent<RTSUnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.H)) {
            rtsUnitController.FreezeSelected();
        }
        if(Input.GetKey(KeyCode.A)) {
            //rtsUnitController.AttackMonster();
        }
    }
}
