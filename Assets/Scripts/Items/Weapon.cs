using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IItem {

    public Weapons weapon;

	public void PickEffect()
    {
        GameManager.instance.player.GetComponentInChildren<PlayerShooting>().ChangeWeapon(weapon);
    }
}
