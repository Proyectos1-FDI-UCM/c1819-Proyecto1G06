using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContact : MonoBehaviour {

    public float knockbackAmount = 5f;  // Fuerza de contacto que aplica

    /// <summary>
    /// Al entrar en contacto con el jugador, le resta vida y le empuja
    /// </summary>
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        Knockback knockback = other.gameObject.GetComponent<Knockback>();

        if(playerHealth != null)
        {
            GameManager.instance.onPlayerTookDamage();
            if(knockback != null)
            {
                knockback.TakeKnockback(-other.contacts[0].normal, knockbackAmount);
            }
        }
    }
}
