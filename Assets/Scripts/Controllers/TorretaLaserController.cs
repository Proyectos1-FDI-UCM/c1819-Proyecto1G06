using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserController : TorretaController {

    public float lerpTime;

    private Vector3 startPosition, currentPosition;

    LineRenderer lr;

    GameObject head;

    public Material redLaser, orangeLaser;

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
        currentPosition = Vector3.Lerp(startPosition, player.transform.position, (Time.time) / lerpTime);
        lr.SetPosition(0, head.transform.position);
        lr.SetPosition(1, currentPosition);

        if (state == EnemyState.Shooting)
        {
            lr.enabled = true;

            if (!shot)
            {
                Vector2 lookDirection = (currentPosition) - transform.position;
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

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        Vector3 result = Vector3.Slerp(start, end, percentageComplete);

        return result;
    }

    void CheckCollisions()
    {
        hit = Physics2D.Linecast(head.transform.position, currentPosition, LayerMask.GetMask("Player"));
        if (hit.transform.GetComponent<PlayerHealth>() != null)
        {
            hit.transform.GetComponent<PlayerHealth>().TakeDamage();
            Debug.Log(hit.transform.GetComponent<PlayerHealth>());
            ShootLaser();
            Invoke("StopShootingLaser", 0.5f);
        }
    }

    void ShootLaser()
    {
        shot = true;
        lr.material = redLaser;
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
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
