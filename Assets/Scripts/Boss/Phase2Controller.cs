using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Controller : EnemyController {

    FollowDirection follow;
    Animator anim;

    private void Awake()
    {
        follow = GetComponent<FollowDirection>();
        state = EnemyState.Chasing;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Chasing:
                follow.MoveTowards((player.transform.position - transform.position));
                break;
            case EnemyState.Shooting:
                follow.Stop();
                break;
        }       
    }
}
