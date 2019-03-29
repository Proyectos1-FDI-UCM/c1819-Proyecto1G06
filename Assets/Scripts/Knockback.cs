using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    Rigidbody2D rb;

    void Awake () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}

    /// <summary>
    /// Recibe amount de knockback en la dirección dir
    /// </summary>
    public void TakeKnockback(Vector2 dir, float amount)
    {
        rb.AddForce(amount * dir.normalized, ForceMode2D.Impulse);
    }
}
