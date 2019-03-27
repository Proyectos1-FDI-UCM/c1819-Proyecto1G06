using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioController : EnemyController {

    public Vector2 lurkInterval = new Vector2(4, 6);

    FollowDirection follow;
    Vector2 moveDir = Vector2.zero;
    Vector2 avoidDir;

    private void Awake()
    {
        follow = GetComponent<FollowDirection>();
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Fleeing:
                moveDir = (transform.position - player.transform.position).normalized;
                break;
            case EnemyState.Chasing:
                moveDir = (player.transform.position - transform.position).normalized;
                break;
        }

        moveDir += avoidDir;
        if(moveDir.magnitude > 0)
        {
            follow.MoveTowards(moveDir.normalized);
        } else
        {
            follow.Stop();
        }

        moveDir = Vector2.zero;
        avoidDir = Vector2.zero;
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

    public void AvoidDirection(Vector2 dir)
    {
        avoidDir = dir;
    }
}
