using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TORv0Controller : EnemyController {

    public Vector2 waitRange;       //Los dos números que delimitan el tiempo que tendrá que esperar el TORv0

    TORv0Health health;
    FollowDirection follow;
    Rigidbody2D rb;
    Animator anim;

	void Awake()
    {
        follow = GetComponent<FollowDirection>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = GetComponent<TORv0Health>();
    }

    public void Start()
    {
        //Inicia el comportamiento del TORv0
        Invoke("Chase", Random.Range(waitRange.x, waitRange.y));
    }

    /// <summary>
    /// Cambia el estado y animación del TORv0, lo para y le perimite recibir daño.
    /// </summary>
    void Stun()
    {
        rb.velocity = Vector2.zero;
        state = EnemyState.Stunned;
        anim.SetTrigger("Stunned");
        health.MakeVulnerable(state);
    }

    /// <summary>
    /// Cambia estado y animacón, le hace invulnerable y hace que embista de nuevo en entre waitRange.x y waitRange.y segundos.
    /// </summary>
    void UnStun()
    {
        state = EnemyState.Idle;
        health.MakeVulnerable(state);
        Invoke("Chase", Random.Range(waitRange.x, waitRange.y));
    }

    /// <summary>
    /// Embiste al jugador, le cambia la animación y el estado.
    /// </summary>
    void Chase()
    {
        anim.SetTrigger("Chase");
        state = EnemyState.Chasing;
    }

    public void Move()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        follow.MoveTowards(dir);
        if (dir.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else GetComponent<SpriteRenderer>().flipX = false;
    }

    /// <summary>
    /// Al colisionar con algo se stunea.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CancelInvoke("Chase");
        if (state != EnemyState.Stunned)
            Stun();
    }
}
