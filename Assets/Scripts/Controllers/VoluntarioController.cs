using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioController : EnemyController {

    public Vector2 lurkInterval = new Vector2(4, 6);

    FollowDirection follow;

    private void Awake()
    {
        follow = GetComponent<FollowDirection>();
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Fleeing:
                follow.MoveTowards((transform.position - player.transform.position).normalized);
                break;
            case EnemyState.Chasing:
                follow.MoveTowards((player.transform.position - transform.position).normalized);
                break;
            case EnemyState.Idle:
                follow.Stop();
                break;
        }
    }

    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (playerDetected)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < lurkInterval.x)
            {
                state = EnemyState.Fleeing;
            } else if(Vector3.Distance(player.transform.position, transform.position) > lurkInterval.y)
            {
                state = EnemyState.Chasing;
            } else
            {
                state = EnemyState.Idle;
            }
        }
    }
}
