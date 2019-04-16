using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    /// <summary>
    /// Al chocar con el jugador, le resta vida. Se destruye
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            GameManager.instance.onPlayerTookDamage();
        }

        if (other.GetComponent<EnemyHealth>() == null)
            Destroy(gameObject);
    }
}
