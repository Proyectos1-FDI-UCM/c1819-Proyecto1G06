using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisarmingBullet : PlayerBullet {

    public float disarmDuration;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Shooting shooting = collision.gameObject.GetComponentInChildren<Shooting>();
        if(shooting != null && collision.GetComponentInChildren<PlayerShooting>() == null)
        {
            shooting.Disarm(disarmDuration);
        }

        base.OnTriggerEnter2D(collision);
    }
}
