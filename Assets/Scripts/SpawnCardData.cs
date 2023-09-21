using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCardData : ScriptableObject
{
    public enum AttributeType {
        Fire, Ice, Light, Darkness
   }
    [SerializeField] private AttributeType attributeType;
    public AttributeType Type { get; set; }

    [SerializeField] private int gold;
    public int Gold { get; set; }

    [SerializeField] private int grade;
    public int Grade { get; set; }


}
