using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Controller : EnemyController {

    FollowDirection follow;
    Vector2 moveDir = Vector2.zero;
    Vector2 avoidDir;

    private void Awake()
    {
        follow = GetComponent<FollowDirection>();
        state = EnemyState.Chasing;
    }

    /// <summary>
    /// Si está en fleeing, se mueve contrario al jugador, si no, se mueve hacia él
    /// Actualiza el tiempo de melee
    /// Dispara
    /// </summary>
    private void Update()
    {

        switch (state)
        {
            case EnemyState.Chasing:
                moveDir = (player.transform.position - transform.position).normalized;
                break;
            case EnemyState.Shooting:
                follow.Stop();
                break;
        }
        //Se suma avoidDir para hacer un movimiento compuesto
        moveDir += avoidDir;
        if (moveDir.magnitude > 0)
        {
            follow.MoveTowards(moveDir);
        }
        else
        {
            follow.Stop();
        }

        moveDir = Vector2.zero;
        avoidDir = Vector2.zero;
    }

    /// <summary>
    /// Hace que avoidDir sea el vector dado.
    /// </summary>
    public void AvoidDirection(Vector2 dir)
    {
        avoidDir = dir;
    }
}
