using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private TestData testData;
    public TestData TestData { set {testData = value;} }


         public void WatchMonsterInfo()
    {   
        Debug.Log("이름 :: " + testData.TestName);
        Debug.Log("HP :: " + testData.HP);
    }

    
}
