using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TORv0Controller : EnemyController {

    public Vector2 waitRange;       //Los dos números que delimitan el tiempo que tendrá que esperar el TORv0
    public float chaseTime = 3f;
    public AudioClip whistleClip;

    TORv0Health health;
    FollowDirection follow;
    Animator anim;
    AudioSource audioSource;

    float _chaseTime;

	void Awake()
    {
        follow = GetComponent<FollowDirection>();
        anim = GetComponent<Animator>();
        health = GetComponent<TORv0Health>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        //Inicia el comportamiento del TORv0
        Invoke("Chase", Random.Range(waitRange.x, waitRange.y));
    }

    private void Update()
    {
        if(state == EnemyState.Chasing) 
        {
            if (_chaseTime > 0)
            {
                _chaseTime -= Time.deltaTime;
            } else
            {
                _chaseTime = 0;
                Stun();
            }
        }
    }

    /// <summary>
    /// Cambia el estado y animación del TORv0, lo para y le perimite recibir daño.
    /// </summary>
    void Stun()
    {
        follow.HardStop();
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
        audioSource.PlayOneShot(whistleClip);
        CancelInvoke("Chase");      
        Invoke("Chase", Random.Range(waitRange.x, waitRange.y));       
    }

    /// <summary>
    /// Embiste al jugador, le cambia la animación y el estado.
    /// </summary>
    void Chase()
    {
        _chaseTime = chaseTime;
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
