using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKIIShooting : MkIShooting
{
    public float deviation = 1f;

    /// <summary>
    /// Dispara una bala en una dirección con una desviación
    /// </summary>
    public override void Shot()
    {
        ResetCooldown();
        BulletMovement newBullet = Instantiate<BulletMovement>(bulletPrefab, shootingPoint.position, Quaternion.identity, bulletPool);
        newBullet.Rotate(transform.right + new Vector3(Random.Range(-deviation / 2, deviation / 2), Random.Range(-deviation / 2, deviation / 2), 0));
        audioSource.PlayOneShot(shootClip);
        shooting = false;     
    }
}
