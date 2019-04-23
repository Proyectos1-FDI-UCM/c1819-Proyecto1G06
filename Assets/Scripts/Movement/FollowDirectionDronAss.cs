using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDirectionDronAss : FollowDirection {

    public override void MoveTowards(Vector2 direction)
    {
        base.MoveTowards(direction);
        if(direction.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
