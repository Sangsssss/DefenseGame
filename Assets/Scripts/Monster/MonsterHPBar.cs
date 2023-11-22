using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
    private Monster monster;
    // Hp
    [SerializeField] Slider HPBar;
    [SerializeField] private Vector3 hpBarOffset; // HP 바의 위치 오프셋
    [SerializeField] private Quaternion hpBarRotation;
    private double maxHP;
    

    // Start is called before the first frame update
    void Awake() {
        monster = this.GetComponent<Monster>();
    }

    void Start() {
        maxHP  = monster.HP;
    }
    // Bar UI Rotation 고정
    void LateUpdate()
    {
        HPBar.transform.rotation = hpBarRotation;
    }

    // Update is called once per frame
    void Update()
    {   
        HPBar.transform.position = transform.position + hpBarOffset;
        UpdateHP();
    }

    
    private void UpdateHP() {
        HPBar.value = (float)(monster.CurrentHP / maxHP);
    }


}

