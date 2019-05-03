using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandom : MonoBehaviour {

    public float changeDirectionEvery = 3f;     // Tiempo entre cambios de dirección
    public float speed = 2f;

    FollowDirection follow;

    float changeDirectionCooldown = 0;

    private void Awake()
    {
        follow = GetComponent<FollowDirection>();
    }

    /// <summary>
    /// Al iniciarse, pone el tiempo entre cambios al máximo
    /// </summary>
    private void OnEnable()
    {
        changeDirectionCooldown = changeDirectionEvery;
    }

    /// <summary>
    /// Se mueve en una dirección aleatoria y actualiza el cooldown
    /// </summary>
    void Update ()
    {
        if (changeDirectionCooldown <= 0)
        {
            follow.MoveTowards(CalculateDirection());
            changeDirectionCooldown = changeDirectionEvery;
        }
        else
            changeDirectionCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Consigue una dirección aleatoria
    /// </summary>
    public Vector2 CalculateDirection()
    {
        Vector2 vector2;
        do
        {
            vector2 = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        } while (vector2.magnitude <= 0);
        return vector2;
    }
}
