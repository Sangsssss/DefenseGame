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
        var test = Spawn();
        test.WatchMonsterInfo();
    }

    public Test Spawn() {
        var newTest = Instantiate(testPrefab).GetComponent<Test>();
        newTest.testData = testData;
        newTest.name = newTest.testData.name;
        return newTest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
