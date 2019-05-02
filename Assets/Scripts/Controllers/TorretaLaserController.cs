using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserController : TorretaController {

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
        shooting = GetComponentInChildren<TorretaLaserShooting>();
    }

    private void Update()
    {
        if (playerDetected)
        {
            lr.SetPosition(0, shooting.shootingPoint.transform.position);
            lr.SetPosition(1, Physics2D.Raycast(shooting.shootingPoint.transform.position, shooting.transform.right, Mathf.Infinity, LayerMask.GetMask("Player", "Environment")).point);
            lr.enabled = true;
        }
        else
        {
            lr.enabled = false;
        }
        switch (state)
        {
            case EnemyState.Shooting:
                shooting.Cooldown();
                shooting.Shoot();
                break;
        }
    }
}
