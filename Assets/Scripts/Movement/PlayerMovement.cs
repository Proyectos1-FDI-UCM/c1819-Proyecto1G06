using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Range(0f, 1f)]
    public float decelerationMultiplier = 0.9f;     //El multiplicador que indica la velocidad a la que decelera.
    public float maxVelocity = 5f;                  //La velocidad máxima que puede alcanzar.                   
                                                    //maxVelocity y accelerationMultiplier deben estar ligadas para que el jugador responda correctamente
    public float VelocityToAccelerationRatio = 1.5f / 7f;
    public float maxAbsoluteVelocity = 12f;

    float accelerationMultiplier = 0.9f;            //El multiplicador del impulso, es decir, lo rápido que acelera.
    bool canLoseSpeed = true;

    Rigidbody2D rb;
    Vector2 movement = Vector2.zero;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameManager.instance.player = gameObject;
    }

    private void Start()
    {
        accelerationMultiplier = maxVelocity * VelocityToAccelerationRatio;
        GameManager.instance.ui.UpdateSpeed(maxVelocity);
    }

    /// <summary>
    /// Se detecta el input
    /// </summary>
    void Update () {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    /// <summary>
    /// Se mueve en dirección movement, frenado por la fricción
    /// </summary>
    private void FixedUpdate()
    {
        movement.Normalize();

        if (movement.magnitude > 0.1f && rb.velocity.magnitude < maxVelocity)
        {
            rb.velocity += movement * accelerationMultiplier;
        }

        if (rb.velocity.magnitude > 0f)
        {
            rb.velocity *= decelerationMultiplier;
        }
    }

    /// <summary>
    /// Aumenta la velocidad del jugador si no está en la máxima
    /// </summary>
    public void AddSpeed(float amount)
    {
        if(amount < 0)
        {
            if (canLoseSpeed)
            {
                maxVelocity += amount;
                if (maxVelocity > maxAbsoluteVelocity) maxVelocity = maxAbsoluteVelocity;
                accelerationMultiplier = maxVelocity * VelocityToAccelerationRatio;
                GameManager.instance.ui.UpdateSpeed(maxVelocity);
            }
        }
        else
        {
            maxVelocity += amount;
            if (maxVelocity > maxAbsoluteVelocity) maxVelocity = maxAbsoluteVelocity;
            accelerationMultiplier = maxVelocity * VelocityToAccelerationRatio;
            GameManager.instance.ui.UpdateSpeed(maxVelocity);
        }
    }

    public void InvertCanLoseSpeed()
    {
        canLoseSpeed = !canLoseSpeed;
    }
}
