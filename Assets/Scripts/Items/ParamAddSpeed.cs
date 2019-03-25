using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamAddSpeed : MonoBehaviour, IItem {

    public float amount;

    /// <summary>
    /// Avisa de que añada velocidad al jugador
    /// </summary>
    public void PickEffect()
    {
        ItemManager.instance.AddSpeed(amount);
    }
}
