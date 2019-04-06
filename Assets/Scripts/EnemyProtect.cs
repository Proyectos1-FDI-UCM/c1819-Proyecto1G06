using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProtect : MonoBehaviour
{
    public Transform Amigo;
    public Transform FollowPoint;
    ColliderDistance2D colDistance;
    FollowDirection follow;
    public Transform player;

    void Start()
    {
        follow = GetComponent<FollowDirection>();    
    }

    void Update()
    {
        colDistance = Physics2D.Distance(FollowPoint.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
        if (Amigo != null)
        {
            if (colDistance.distance >= 0.05)
            {
                follow.MoveTowards(FollowPoint.position - transform.position);
            }
            else
            {
                transform.position = FollowPoint.position;
            }
            
        }
    }
}
