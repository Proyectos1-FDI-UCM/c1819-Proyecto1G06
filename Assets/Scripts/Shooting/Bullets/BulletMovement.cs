using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float speed = 5f;               

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Aplica la velocidad a la bala hacia su derecha.
    /// </summary>
    private void Start()
    {
        rb.velocity = (transform.right * speed);
    }


    /// <summary>
    /// Cambia la derecha de la bala para que coincida con rotation.
    /// </summary>
    public void Rotate(Vector3 rotation)
    {
        transform.right = rotation;
    }
}
