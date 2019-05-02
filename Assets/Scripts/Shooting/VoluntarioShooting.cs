using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioShooting : MkIShooting
{
    /// <summary>
    /// Dispara una bala
    /// </summary>
    public override void Shot()
    {
        ResetCooldown();
        BulletMovement newBullet = Instantiate<BulletMovement>(bulletPrefab, shootingPoint.position, Quaternion.identity, bulletPool);
        newBullet.Rotate(transform.right);
        audioSource.PlayOneShot(shootClip);
    }

    public void StopShooting()
    {
        shooting = false;
    }
}
