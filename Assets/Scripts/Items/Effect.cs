using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, IItem {

    public BulletEffects effect;

	/// <summary>
    /// Cambia el efecto de la bala.
    /// </summary>
    public void PickEffect()
    {
        GameManager.instance.player.GetComponentInChildren<PlayerShooting>().ChangeEffect(effect, GetComponent<ItemData>());
    }
}
