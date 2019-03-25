using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float maxHealth = 10;
    protected float curHealth;

    private void Awake()
    {
        curHealth = maxHealth;
    }

    /// <summary>
    /// Pierde amount de vida hasta morir
    /// </summary>
    public virtual void TakeDamage(float amount)
    {
        curHealth -= amount;

        if (curHealth <= 0)
        {
            curHealth = 0;
            Die();
        }
    }

    /// <summary>
    /// Se destruye el objeto que tenga este componente y avisa que se ha muerto
    /// </summary>
    public virtual void Die()
    {
        SendMessageUpwards("EnemyDied", transform, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
