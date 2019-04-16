using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IItem {

    public Weapons weapon;
    public Sprite spriteWeapon;

	public void PickEffect()
    {
        GameManager.instance.onWeaponChanged(weapon, GetComponent<ItemData>(), spriteWeapon);
    }
}
