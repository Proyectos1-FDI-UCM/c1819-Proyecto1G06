using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelulaShooting : Shooting {

    public int numBalas = 6;
    public float limitAngle = 60f;

    public override void Update()
    {
        base.Update();
        Shoot();
    }

    /// <summary>
    /// Dispara balas en un arco de 2 veces el ángulo límite.
    /// </summary>
    public override void Shoot()
    {
        if (shootCooldown == 0f)
        {
            ResetCooldown();

            float currAngle = -limitAngle;
            
            for(int i = 0; i < numBalas; i++)
            {
                BulletMovement newBullet = Instantiate<BulletMovement>(bulletPrefab, shootingPoint.position, Quaternion.identity, bulletPool);
                newBullet.Rotate(transform.right + new Vector3(Mathf.Cos(currAngle), Mathf.Sin(currAngle), 0));
                currAngle += limitAngle * 2 / numBalas;
            }
        }
    }
}
