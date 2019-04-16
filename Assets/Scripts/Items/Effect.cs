using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, IItem {

    public BulletEffects effect;
    public Sprite effectSprite;

	/// <summary>
    /// Cambia el efecto de la bala.
    /// </summary>
    public void PickEffect()
    {
        GameManager.instance.onEffectChanged(effect, GetComponent<ItemData>(), effectSprite);
    }
}
