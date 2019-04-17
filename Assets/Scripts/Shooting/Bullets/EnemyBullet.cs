using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    /// <summary>
    /// Al chocar con el jugador, le resta vida. Se destruye
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            GameManager.instance.onPlayerTookDamage();
        }

        if (other.GetComponent<EnemyHealth>() == null)
            Destroy(gameObject);
    }
}
