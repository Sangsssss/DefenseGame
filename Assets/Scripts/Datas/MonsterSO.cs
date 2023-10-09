
using UnityEngine;

[System.Serializable]
public class MonsterData {
    public string monsterName;
    public int wave;
    public float health;
    public int damage;
    public int gold;
    public MonsterCommonData monsterCommonData;

    public void ReduceHP(float damage) {
        health -= damage;
    }
}

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]
public class MonsterSO : ScriptableObject
{   
    public MonsterData[] monsterDatas;
    [SerializeField] public MonsterData[] MonsterData {get {return monsterDatas;}}

    // [SerializeField]
    // private float moveSpeed;
    // public float MoveSpeed { get { return moveSpeed; } }

    // [SerializeField]
    // private AudioClip deathSound;

}
