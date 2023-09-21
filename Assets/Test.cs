using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TestData testData;


         public void WatchMonsterInfo()
    {   
        Debug.Log("이름 :: " + testData.TestName);
        Debug.Log("HP :: " + testData.HP);
    }

    
}
