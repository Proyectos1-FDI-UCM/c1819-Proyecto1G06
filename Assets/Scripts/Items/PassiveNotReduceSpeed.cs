using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveNotReduceSpeed : MonoBehaviour, IItem {

	public void PickEffect()
    {
        GameManager.instance.player.GetComponent<PlayerMovement>().InvertCanLooseSpeed();
    }
}
