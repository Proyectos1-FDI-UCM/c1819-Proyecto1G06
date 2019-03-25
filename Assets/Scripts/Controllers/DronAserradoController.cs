using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronAserradoController : EnemyController {

    FollowDirection followDirection;
    MoveRandom moveRandom;

    private void Awake()
    {
        followDirection = GetComponent<FollowDirection>();
        moveRandom = GetComponent<MoveRandom>();
    }

    /// <summary>
    /// Dependiendo del estado, se mueve hacia el jugador o aleatoriamente
    /// </summary>
    private void Update()
    {
        switch (state)
        {
            case EnemyState.Chasing:
                moveRandom.enabled = false;
                followDirection.MoveTowards((player.transform.position - transform.position).normalized);
                break;
            case EnemyState.Idle:
                moveRandom.enabled = true;
                break;
        }
    }

    /// <summary>
    /// Actualiza el estado dependiendo de detected
    /// </summary>
    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (playerDetected)
        {
            state = EnemyState.Chasing;
        } else
        {
            state = EnemyState.Idle;
        }
    }
}
