using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaController : EnemyController {

    private Shooting shooting;

    private void Awake()
    {
        shooting = transform.GetChild(0).GetComponent<TorretaArtilleríaShooting>();
    }

    private void Update()
    {
        //Si está en Shooting intenta disparar y redue el cooldown del disparo
        if (state == EnemyState.Shooting)
        {
            shooting.Cooldown();
            shooting.Shoot();
        }            
    }

    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (playerDetected)
        {
            state = EnemyState.Shooting;
        }
        else
        {
            state = EnemyState.Idle;
        }
    }
}
