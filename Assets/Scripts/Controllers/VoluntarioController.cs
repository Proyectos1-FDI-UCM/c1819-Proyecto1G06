using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioController : EnemyController {

    public Vector2 lurkInterval = new Vector2(4, 6);    //Mínima y máxima distancia del Voluntario al jugador
    public float meleeDistance;
    public float maxMeleeCooldown;

    float meleeCooldown;
    Animator anim;
    FollowDirection follow;
    Vector2 moveDir = Vector2.zero;
    Vector2 avoidDir;

    private void Awake()
    {
        follow = GetComponent<FollowDirection>();
        anim = GetComponent<Animator>();
        meleeCooldown = maxMeleeCooldown;
    }

    /// <summary>
    /// Si está en fleeing, hace que moveDir sea el opuesto al jugador; 
    /// y si no, hace que sea el que va hacia él.
    /// </summary>
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
        //Se suma avoidDir para ahcer un movimiento compuesto.
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

        meleeCooldown -= Time.deltaTime;
        if (meleeCooldown < 0f) meleeCooldown = 0f;
    }

    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (playerDetected)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < lurkInterval.x)
            {
                state = EnemyState.Fleeing;
                if(Vector3.Distance(player.transform.position, transform.position) <= meleeDistance && meleeCooldown == 0)
                {
                    anim.SetTrigger("Punch");
                    meleeCooldown = maxMeleeCooldown;
                }
            } else if(Vector3.Distance(player.transform.position, transform.position) > lurkInterval.y)
            {
                state = EnemyState.Chasing;
            } else
            {
                state = EnemyState.Idle;
            }
        }
    }

    /// <summary>
    /// Hace que avoidDir sea el vector dado.
    /// </summary>
    public void AvoidDirection(Vector2 dir)
    {
        avoidDir = dir;
    }
}
