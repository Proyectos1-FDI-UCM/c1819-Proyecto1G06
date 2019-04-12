using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float maxHealth = 10;
    public float damagedTime = 0.1f;

    protected float curHealth;
    public float CurHealth { get { return curHealth; } }
    Animator anim;

    public virtual void Awake()
    {
        curHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Pierde amount de vida hasta morir
    /// </summary>
    public virtual void TakeDamage(float amount)
    {
        anim.SetLayerWeight(1, 1);
        curHealth -= amount;
        Invoke("ResetLayerWeight", damagedTime);

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

    protected virtual void ResetLayerWeight()
    {
        anim.SetLayerWeight(1, 0);
    }
}
