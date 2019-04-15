using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDirectionForces : FollowDirection {

    /// <summary>
    /// Se mueve hacia direction
    /// </summary>
    /// <param name="direction">La dirección</param>
    public override void MoveTowards(Vector2 direction)
    {
        rb.AddForce(direction.normalized * speed);
    }
}
