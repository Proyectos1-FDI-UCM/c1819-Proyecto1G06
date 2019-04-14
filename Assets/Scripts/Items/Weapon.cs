using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IItem {

    public Weapons weapon;
    public Sprite spriteWeapon;

	public void PickEffect()
    {
        GameManager.instance.player.GetComponentInChildren<PlayerShooting>().ChangeWeapon(weapon, GetComponent<ItemData>(), spriteWeapon);
    }
}
