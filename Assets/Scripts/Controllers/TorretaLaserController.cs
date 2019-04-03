using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserController : TorretaController {

    public float lerpTime;

    private Vector3 startPosition;

    LineRenderer lr;

    GameObject head;

    public Material redLaser, orangeLaser;

    public Transform followPoint;

    bool shot = false;

    RaycastHit2D hit;

    private void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
        head = transform.GetChild(0).gameObject;

    }

    public void Start()
    {
        startPosition = player.transform.position;
    }

    public void Update()
    {
        lr.SetPosition(0, head.transform.position);
        lr.SetPosition(1, followPoint.position);

        Debug.DrawLine(head.transform.position, followPoint.position, Color.yellow);

        if (state == EnemyState.Shooting)
        {
            lr.enabled = true;

            if (!shot)
            {
                Vector2 lookDirection = followPoint.position - transform.position;
                float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI);

                head.transform.eulerAngles = new Vector3(0, 0, angle + (lookDirection.x < 0f ? 180f : 0f));

                
                   CheckCollisions();
            }
        }
        else
        {
            lr.enabled = false;
            StopShootingLaser();
        }
    }

    void CheckCollisions()
    {
        hit = Physics2D.Linecast(head.transform.position, followPoint.position);
        
        if (hit.transform.GetComponent<PlayerHealth>() != null) //No detecta la colision (Opciones, linecast, raycast)
        {
            player.GetComponent<PlayerHealth>().TakeDamage();
            ShootLaser();
            Invoke("StopShootingLaser", 0.5f);
        }

    }

    void ShootLaser()
    {
        shot = true;
        lr.material = redLaser;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        print("Funciona");
    }

    void StopShootingLaser()
    {
        shot = false;
        lr.material = orangeLaser;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;

        lerpTime += Time.time;
    }

    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
    }
}
