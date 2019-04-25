using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKIIController : EnemyController {

    public float minDistance = 10;              // Distancia mínima a la que quiere estar

    private FollowDirection followDirection;
    private Shooting shooting;
    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        followDirection = GetComponent<FollowDirection>();
        shooting = transform.GetChild(0).GetComponent<Shooting>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Dependiendo del estado en el que esté, acualiza su comportamiento
    /// </summary>
    private void Update()
    {
        switch (state)
        {
            case EnemyState.Fleeing:
                followDirection.MoveTowards((transform.position - player.transform.position).normalized);
                break;
            case EnemyState.Shooting:
                shooting.Cooldown();
                shooting.Shoot();
                break;
        }

        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    /// <summary>
    /// Actualiza su estado dependiendo de si ha visto al jugador o está cerca de él
    /// </summary>
    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (playerDetected)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < minDistance)
            {
                state = EnemyState.Fleeing;
                followDirection.Stop();
                shooting.ResetCooldown();
            }
            else
            {
                state = EnemyState.Shooting;
                followDirection.Stop();
            }
        }
        else
        {
            state = EnemyState.Idle;
            followDirection.Stop();
        }
    }
}
