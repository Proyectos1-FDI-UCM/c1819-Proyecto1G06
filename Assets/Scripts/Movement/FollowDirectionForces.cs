using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDirectionForces : FollowDirection {

    public bool flip;

    /// <summary>
    /// Se mueve hacia direction
    /// </summary>
    /// <param name="direction">La dirección</param>
    public override void MoveTowards(Vector2 direction)
    {
        rb.AddForce(direction.normalized * speed);

        if (flip)
        {
            if (direction.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
