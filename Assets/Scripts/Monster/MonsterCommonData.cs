using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Common Data")]
public class MonsterCommonData : ScriptableObject
{
    [SerializeField] private Transform[] wayPoints;
    public Transform[] WayPoints { get {return wayPoints;} }
}
