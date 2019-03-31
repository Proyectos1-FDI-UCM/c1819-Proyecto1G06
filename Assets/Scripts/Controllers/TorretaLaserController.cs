using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserController : EnemyController {

    private LineRenderer lr;
    private GameObject shootingPoint;
    Vector2 laserHitPos = Vector2.zero;

    private void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
        shootingPoint = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    public void ShootLaser() {

        if (state == EnemyState.Shooting)
        {
            lr.enabled = true;
            lr.SetPosition(0, shootingPoint.transform.position);
            lr.SetPosition(1, player.transform.position);
        }
        else
            lr.enabled = false;
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
