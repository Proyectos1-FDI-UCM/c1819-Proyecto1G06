using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDirectionNonFunctional : MonoBehaviour {
    //WIP, NO FUNCIONAL
    //POSIBLEMENTE RELEGADO POR NAVMESH 
    private Rigidbody2D rb;
    public float speed = 5f;
    AvoidWalls avoid;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        avoid = GetComponent<AvoidWalls>();
    }

    /// <summary>
    /// Se mueve hacia la dirección direction si no da a una pared
    /// Si da, utilizará una normal que no de a otra pared.
    /// </summary>
    public void MoveTowards(Vector2 direction)
    {
        Vector2 normal;
        Vector2 normal2;
        if (!avoid.HitWall(direction, out normal))
        {
            rb.velocity = direction * speed;
        }      
        else if(!avoid.HitWall(new Vector2(- normal.y, normal.x), out normal2))
        {
            rb.velocity = new Vector2(- normal.y, normal.x) * speed;
        }
        else
        {
            rb.velocity = - normal2 * speed;
        }
    }

    /// <summary>
    /// Deja de moverse
    /// </summary>
    public void Stop()
    {
        rb.velocity *= 0.8f;
    }

}
