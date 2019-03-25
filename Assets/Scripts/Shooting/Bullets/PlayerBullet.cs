using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float Damage { get; set; }

    /// <summary>
    /// Hace daño al enemigo. Se destruye
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {   
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();       

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(Damage);
        }

        if (other.GetComponent<PlayerHealth>() == null)
            Destroy(gameObject);
    }
}
