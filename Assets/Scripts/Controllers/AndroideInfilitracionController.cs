using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroideInfilitracionController : EnemyController {

    public float minDistance = 5f;

    FollowDirection follow;
    Animator anim;
    Vector3 playerPos;

    private void Awake()
    {
        follow = GetComponent<FollowDirection>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Chasing:
                anim.SetFloat("Distance", Vector3.Distance(transform.position, playerPos));
                break;
            case EnemyState.Stunned:
                follow.Stop();
                break;
        }
    }

    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (state == EnemyState.Idle && playerDetected && Vector2.Distance(player.transform.position, transform.position) < minDistance)
        {
            anim.SetTrigger("Chase");
            state = EnemyState.Chasing;
            playerPos = GameManager.instance.player.transform.position;
            follow.MoveTowards((playerPos - transform.position).normalized);
        }
    }

    public void GoIdle()
    {
        state = EnemyState.Idle;
    }

    public void Stun()
    {
        state = EnemyState.Stunned;
    }
}
