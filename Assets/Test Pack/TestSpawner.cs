using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject testPrefab;
    [SerializeField] private TestData testData;
    void Start()
    {
        Test newTest = Instantiate(testPrefab).GetComponent<Test>();
        newTest.TestData = testData;
        
        //Test test = Spawn();
        newTest.WatchMonsterInfo();
    }

    public Test Spawn() {
        Test newTest = Instantiate(testPrefab).GetComponent<Test>();
        newTest.TestData = testData;
        // newTest.name = newTest.testData.name;
        return newTest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
