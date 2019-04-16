using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeinRed : MonoBehaviour {

    // delegate con metodo Take damage de playerhealth

    public float amount;

    /// <summary>
    /// Avisa de que añada velocidad al jugador
    /// </summary>
    public void PickEffect()
    {
        // if (vidaJugador <= 2 && metodo Take Damage ejecutado)
        GameManager.instance.player.GetComponent<PlayerShooting>().AddDamage(amount/*(Max Health - Health)/2f*/);
    } 
}
