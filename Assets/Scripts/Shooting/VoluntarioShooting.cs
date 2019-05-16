using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioShooting : MkIShooting
{
    public BulletMovement voluntarioCounterBullet;

    public override void Awake()
    {
        base.Awake();
    }

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

    public void ShotCounter()
    {
        BulletMovement newBullet = Instantiate<BulletMovement>(voluntarioCounterBullet, shootingPoint.position, Quaternion.identity, bulletPool);
        newBullet.Rotate(transform.right);
        audioSource.PlayOneShot(shootClip);
    }

    public void CounterWeapon()
    {
        anim.SetTrigger("Counter");
    }
}
