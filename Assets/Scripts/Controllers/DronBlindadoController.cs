using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronBlindadoController : MonoBehaviour
{
    public Transform Amigo;
    public Transform FollowPoint;

    DronAserradoController dronAss;
    ColliderDistance2D colDistance;
    FollowDirection follow;
    Transform player;

    void Start()
    {
        follow = GetComponent<FollowDirection>();
        player = GameManager.instance.player.transform;
        dronAss = GetComponent<DronAserradoController>();
    }

    void Update()
    {
        if (Amigo != null)
        {
            if (Vector2.Distance(FollowPoint.position, player.position) > 0.1f)
            {
                follow.MoveTowards(FollowPoint.position - transform.position);
            }
            else
            {
                follow.Stop();
            }

        }
        else
        {
            dronAss.enabled = true;
            this.enabled = false;
        }
    }
}
