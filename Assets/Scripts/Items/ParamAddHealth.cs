using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamAddHealth : MonoBehaviour, IItem {

    public int amount;
    [Tooltip("Whether it should also heal the player by that amount")]
    public bool heal;

    /// <summary>
    /// Avisa de que añada vida al jugador
    /// </summary>
    public void PickEffect()
    {
        GameManager.instance.player.GetComponent<PlayerHealth>().AddMaxHealth(amount, heal);
    }
}
