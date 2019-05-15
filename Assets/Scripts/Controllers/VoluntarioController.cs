using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioController : EnemyController {

    public Vector2 lurkInterval = new Vector2(4, 6);    //Mínima y máxima distancia del Voluntario al jugador
    public float meleeDistance;
    public float meleeCooldown = 3;

    float meleeTimer;
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
                case EnemyState.Idle:
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

            Cooldown(ref meleeTimer);

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
        } else
        {
            state = EnemyState.Idle;
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
        if (shooting.GetShooting())
        {
            Stun();
        }
        else if (!stunned)
        {
            CounterWeapon();
            ResetShootCooldown();
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

    public void ShootCounter()
    {
        shooting.ShotCounter();
    }

    public void ResetShootCooldown()
    {
        shooting.ResetCooldown();
    }

    public void CounterWeapon()
    {
        shooting.CounterWeapon();
    }
}
