using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data")]
public class MonsterData : ScriptableObject
{   
    // Start is called before the first frame update
    [SerializeField]
    private string monsterName;
    public string MonsterName { get; set; }
    [SerializeField]
    private int wave;
    public int Wave { get; set; }
    [SerializeField]
    private int health;
    public int Health { get; set; }

    [SerializeField]
    private int damage;
    public int Damage { get; set; }

    [SerializeField]
    private int gold;
    public int Gold {  get; set; }

    [SerializeField] private MonsterCommonData commonData;
    public MonsterCommonData CommonData {  get; set; }


    // [SerializeField]
    // private float moveSpeed;
    // public float MoveSpeed { get { return moveSpeed; } }

    // [SerializeField]
    // private AudioClip deathSound;

}
