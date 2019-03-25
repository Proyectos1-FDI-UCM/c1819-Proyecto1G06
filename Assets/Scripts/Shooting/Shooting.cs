using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{   
    public float rateOfFire = 1f;                   //Numero de balas por segundo
    public Transform shootingPoint;                 //Punto de donde sale la bala
    public BulletMovement bulletPrefab;             //Script del movimiento de la bala

    protected float shootCooldown = 0f;             //Tiempo hasta la siguiente bala
    protected Transform player { get { return GameManager.instance.player.transform; } }
    protected Transform bulletPool { get { return GameManager.instance.bulletPool; } }                 //Padre de las balas

    public virtual void Update()
    {
        Cooldown();
    }

    /// <summary>
    /// Reduce shootCooldown cada segundo
    /// </summary>
    public void Cooldown()
    {
        if (shootCooldown > 0f)
            shootCooldown -= Time.deltaTime;        //Se reduce si no es 0

        else
            shootCooldown = 0f;
    }

    /// <summary>
    /// Genera una bala y establece el enfriamiento.
    /// </summary>
    public virtual void Shoot()
    {
        if (shootCooldown == 0f)
        {
            ResetCooldown();
            BulletMovement newBullet = Instantiate<BulletMovement>(bulletPrefab, shootingPoint.position, Quaternion.identity, bulletPool);
            newBullet.Rotate(transform.right);
        }
    }

    /// <summary>
    /// Pone el cooldown en su valor máximo
    /// </summary>
    public virtual void ResetCooldown()
    {
        shootCooldown = 1 / rateOfFire;
    }
}
