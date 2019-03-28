﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioController : EnemyController {

    public Vector2 lurkInterval = new Vector2(4, 6);    //Mínima y máxima distancia del Voluntario al jugador
    public float meleeDistance;
    public float meleeCooldown = 3;
    public float stunCooldown = 3;
    public float counterCooldown = 3;

    float meleeTimer;
    float stunTimer;
    float counterTimer;
    Animator anim;
    FollowDirection follow;
    VoluntarioShooting shooting;
    Vector2 moveDir = Vector2.zero;
    Vector2 avoidDir;
    bool stunned = false;

    private void Awake()
    {
        follow = GetComponent<FollowDirection>();
        anim = GetComponent<Animator>();
        shooting = transform.GetChild(0).GetComponent<VoluntarioShooting>();
        meleeTimer = meleeCooldown;
        stunTimer = stunCooldown;
        counterTimer = counterCooldown;
    }

    /// <summary>
    /// Si está en fleeing, se mueve contrario al jugador, si no, se mueve hacia él
    /// Actualiza el tiempo de melee
    /// Dispara
    /// </summary>
    private void Update()
    {
        if (!stunned)
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
            //Se suma avoidDir para hacer un movimiento compuesto
            moveDir += avoidDir;
            if (moveDir.magnitude > 0)
            {
                follow.MoveTowards(moveDir.normalized);
            }
            else
            {
                follow.Stop();
            }

            moveDir = Vector2.zero;
            avoidDir = Vector2.zero;

            Cooldown(ref meleeTimer);
            Cooldown(ref stunTimer);
            Cooldown(ref counterTimer);

            shooting.Cooldown();
            shooting.Shoot();
        } else
        {
            follow.Stop();
        }
    }

    void Cooldown(ref float timer)
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else timer = 0;
    }

    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (playerDetected)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < lurkInterval.x)   // Jugador cerca
            {
                state = EnemyState.Fleeing;
                if(Vector3.Distance(player.transform.position, transform.position) <= meleeDistance && meleeTimer <= 0)
                {
                    anim.SetTrigger("Punch");
                    meleeTimer = meleeCooldown;
                }
            } else if(Vector3.Distance(player.transform.position, transform.position) > lurkInterval.y) // Jugador lejos
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

    public bool BulletHit()
    {
        print(stunTimer);
        if (shooting.GetShooting() && stunTimer <= 0)
        {
            stunTimer = stunCooldown;
            Stun();
        } else if(counterTimer <= 0)
        {
            counterTimer = counterCooldown;
            anim.SetTrigger("Counterattack");
        }
        return stunned;
    }

    public void Stun()
    {
        stunned = true;
        anim.SetTrigger("Stun");
    }

    public void Unstun()
    {
        stunned = false;
    }

    public void Shoot()
    {
        shooting.Shot();
    }
}
