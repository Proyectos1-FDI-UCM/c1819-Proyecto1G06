using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MkIController : EnemyController
{
    public float minDistance = 10;              // Distancia mínima a la que quiere estar

    private LineRenderer lr;
    private FollowDirection followDirection;
    private Shooting shooting;
    GameObject weapon;
    Vector2 laserHitPos = Vector2.zero;
    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
        followDirection = GetComponent<FollowDirection>();
        shooting = transform.GetChild(0).GetComponent<Shooting>();
        weapon = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                anim.SetFloat("Velocity", rb.velocity.magnitude);
                break;
            case EnemyState.Shooting:
                lr.SetPosition(0, weapon.transform.position);
                lr.SetPosition(1, laserHitPos);
                shooting.Cooldown();
                shooting.Shoot();
                break;
        }
    }

    /// <summary>
    /// Actualiza su estado dependiendo de si ha visto al jugador o está cerca de él
    /// </summary>
    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        laserHitPos = sight.point;
        if (playerDetected)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < minDistance)
            {
                state = EnemyState.Fleeing;
                followDirection.Stop();
                lr.enabled = false;
                shooting.ResetCooldown();
            }
            else
            {
                state = EnemyState.Shooting;
                lr.enabled = playerDetected;
                followDirection.Stop();
            }
        }
        else
        {
            state = EnemyState.Idle;
            followDirection.Stop();
            lr.enabled = false;
        }
    }
}
