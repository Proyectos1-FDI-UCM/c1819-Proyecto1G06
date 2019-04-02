using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserController : TorretaController {

    private LineRenderer lr;
    TorretaLaserShooting laserShooting;
    FollowDirection follow;
    public float timeOffset = 0.5f;
    GameObject head;
    public Material RedLaser, OrangeLaser;
    public Transform shootingPoint;
    RaycastHit2D hit;

    Vector3 currentPosition;

    private void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
        laserShooting = GetComponentInChildren<TorretaLaserShooting>();
        head = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {

        // Set our position as a fraction of the distance between the markers.
        currentPosition =laserShooting.Lerp(laserShooting.startPosition, laserShooting.endPosition.position, laserShooting.lerpTime, laserShooting.timeStartedLerping);

        lr.SetPosition(0, head.transform.position);
        lr.SetPosition(1, currentPosition);

        if (state == EnemyState.Shooting)
        {
            laserShooting.Cooldown();
            lr.enabled = true;
            if (lr.GetPosition(1) == player.transform.position)
            {            
                ShootLaser();
                CheckCollisions();
                Invoke("StopShootingLaser", timeOffset);
            }
        }
        else
        {
            lr.enabled = false;
            laserShooting.timeStartedLerping = Time.time;
        }
            
    }

    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (playerDetected)
            state = EnemyState.Shooting;
        else
            state = EnemyState.Idle;
    }

    void ShootLaser()
    {
        laserShooting.found = true;
        laserShooting.Shoot();
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        lr.material = RedLaser;
    }

    void StopShootingLaser()
    {
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = OrangeLaser;
        laserShooting.timeStartedLerping = Time.time;
        laserShooting.found = false;
    }

    private void OnDisable()
    {
        lr.enabled = false;
    }

    private void CheckCollisions()
    {
        lr.SetPosition(1, currentPosition);
        hit = Physics2D.Linecast(shootingPoint.position, currentPosition, LayerMask.GetMask("Player"));
        if (hit.transform.GetComponent<PlayerHealth>() != null)
            hit.transform.GetComponent<PlayerHealth>().TakeDamage();

    }
}
