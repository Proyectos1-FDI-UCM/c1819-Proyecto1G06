using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDirection : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Se mueve hacia direction
    /// </summary>
    /// <param name="direction">La dirección</param>
    public void MoveTowards(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }

    /// <summary>
    /// Deja de moverse
    /// </summary>
    public void Stop()
    {
        rb.velocity *= 0.8f;
    }

    /// <summary>
    /// Para completamente al objeto
    /// </summary>
    public void HardStop()
    {
        rb.velocity = Vector2.zero;
    }
}
