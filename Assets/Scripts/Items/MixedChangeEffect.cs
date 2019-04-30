using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedChangeEffect : MonoBehaviour, IItem {

    public BulletEffects effect;

	/// <summary>
    /// Para sobreescribir directamente el efecto; útil para el Ilegal.
    /// </summary>
    public void PickEffect()
    {
        ItemManager.instance.OverrideEffect(effect);
    }
}
