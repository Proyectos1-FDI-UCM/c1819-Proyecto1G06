using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserShooting : MkIShooting {

    bool damaging = false;
    LineRenderer lr;

    public override void Awake()
    {
        lr = GetComponent<LineRenderer>();
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if (!shooting)
        {
            if (controller.GetPlayerDetected())
            {
                MoveTurret();

                lr.SetPosition(0, shootingPoint.transform.position);
                lr.enabled = true;
            }
            else
            {
                lr.enabled = false;
            }
        }

        if (damaging)
        {
            RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position, transform.right, Mathf.Infinity, LayerMask.GetMask("Environment", "Player"));
            if (hit.transform == player)
            {
                GameManager.instance.onPlayerTookDamage();
            }
        }

        lr.SetPosition(1, Physics2D.Raycast(shootingPoint.transform.position, transform.right, Mathf.Infinity, LayerMask.GetMask("Player", "Environment")).point);
    }

    public void StartDamaging()
    {
        damaging = true;       
    }

    public void StopDamaging()
    {
        damaging = false;
    }

    public void StopShooting()
    {
        shooting = false;
        ResetCooldown();
    }

    void MoveTurret()
    {
        Vector2 lookDirection = player.position - transform.position;
        float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI) + (lookDirection.x < 0f ? 180f : 0f);

        Vector2 bodyLookDir = player.position - body.position;
        float bodyAngle = Mathf.Atan(bodyLookDir.y / bodyLookDir.x) * (180 / Mathf.PI) + (bodyLookDir.x < 0f ? 180f : 0f);

        if (bodyAngle > 90 || bodyAngle < -90)
        {
            // Hacer que no tenga un movimiento poco natural
            //transform.localScale = new Vector3(-1, 1, 1);
            body.localScale = new Vector3(1, 1, 1);
            sprite.flipY = true;
        }
        else
        {
            //transform.localScale = new Vector3(1, 1, 1);
            body.localScale = new Vector3(-1, 1, 1);
            sprite.flipY = false;
        }

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void PlayClip()
    {
        audioSource.PlayOneShot(shootClip);
    }

}
