using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Test Data", menuName = "Scriptable Object/Test Data", order = int.MaxValue)]
public class TestData : ScriptableObject
{
    [SerializeField] string testName;
    public string TestName {get {return testName;}}
    [SerializeField] int hp;
    public int HP {get {return hp;}}

}
