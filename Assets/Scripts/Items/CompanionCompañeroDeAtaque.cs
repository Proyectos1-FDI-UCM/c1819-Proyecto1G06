using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCompañeroDeAtaque : Shooting {

    Transform target;
    public float damage = 1;

    public override void Update()
    {
        base.Update();
        if(target != null)
        {
            Shoot();
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if(target == null && collision.GetComponent<EnemyHealth>() != null)
        {
            target = collision.transform;
        }
    }

    public override void Shoot()
    {
        if(shootCooldown == 0f && !disarmed)   //Si puede disparar
        {
            ResetCooldown();
            BulletMovement newBullet = Instantiate<BulletMovement>(bulletPrefab, shootingPoint.position, Quaternion.identity, bulletPool);
            newBullet.GetComponent<PlayerBullet>().Damage = damage;
            newBullet.Rotate(target.position - transform.position);
        }
    }
}
