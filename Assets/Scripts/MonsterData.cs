
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{   
    // Start is called before the first frame update
    [SerializeField]
    private string monsterName;
    public string MonsterName { get {return monsterName;} }
    [SerializeField]
    private int wave;
    public int Wave { get {return wave;} set { wave = value;} }
    [SerializeField]
    private float health;
    public float Health { get {return health;}  set { health = value;} }

    [SerializeField]
    private int damage;
    public int Damage { get {return damage;} set { damage = value;} }

    [SerializeField]
    private int gold;
    public int Gold {  get {return gold;} set { gold = value;} }

    [SerializeField] private MonsterCommonData commonData;
    public MonsterCommonData CommonData { get {return commonData;} }


    // [SerializeField]
    // private float moveSpeed;
    // public float MoveSpeed { get { return moveSpeed; } }

    // [SerializeField]
    // private AudioClip deathSound;

}
