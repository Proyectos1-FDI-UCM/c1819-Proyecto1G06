using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedChangeEffect : MonoBehaviour, IItem {
     
	/// <summary>
    /// Para sobreescribir directamente el efecto; útil para el Ilegal.
    /// </summary>
    public void PickEffect()
    {
        ItemManager.instance.OverrideEffect(GetComponent<ItemData>());
    }
}
