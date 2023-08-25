using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttribute : MonoBehaviour
{
    public enum CardType
    {
        Fire,
        Ice,
        Light,
        Darkness
    }

    public CardType cardType;
}
