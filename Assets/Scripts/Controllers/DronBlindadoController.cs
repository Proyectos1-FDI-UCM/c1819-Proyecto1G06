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

    void Start()
    {
        follow = GetComponent<FollowDirection>();
        dronAss = GetComponent<DronAserradoController>();
    }

    void Update()
    {
        if (Amigo != null)
        {
            if (Vector2.Distance(FollowPoint.position, transform.position) > 0.1f)
            {
                follow.MoveTowards(FollowPoint.position - transform.position);
            }
            else
            {
                follow.HardStop();
            }

        }
        else
        {
            dronAss.enabled = true;
            this.enabled = false;
        }
    }
}
