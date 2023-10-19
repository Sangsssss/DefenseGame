using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{

    public void Onclick()
	{
		this.GetComponent<Animator>().SetTrigger("Flip");
	}
}
