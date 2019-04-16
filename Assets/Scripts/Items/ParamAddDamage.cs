using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamAddDamage : MonoBehaviour, IItem {

    public float amount;

    /// <summary>
    /// Avisa que aumente el daño al jugador
    /// </summary>
    public void PickEffect()
    {
        GameManager.instance.onPlayerAddedDamage(amount);
    }
}
