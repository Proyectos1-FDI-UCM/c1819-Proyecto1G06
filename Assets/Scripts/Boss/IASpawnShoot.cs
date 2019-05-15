using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASpawnShoot : Shooting {

    public Transform[] cannons;

    public override void Update()
    {
        base.Update();
        if (shootCooldown == 0f && !disarmed) Shoot(); 
    }

    public override void Shoot()
    {
        for(int i = 0; i < cannons.Length; i++)
        {
            ResetCooldown();
            Shoot(i);
        }
    }

    public void Shoot(int i)
    {
        BulletMovement newBullet = Instantiate<BulletMovement>(bulletPrefab, cannons[i].position, Quaternion.identity, bulletPool);
        newBullet.Rotate(cannons[i].right);
    }
}
