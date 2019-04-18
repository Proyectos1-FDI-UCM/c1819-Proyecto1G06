using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{   
    [Tooltip("Balas por segundo.")]
    public float rateOfFire = 1f;                   //Numero de balas por segundo
    public Transform shootingPoint;                 //Punto de donde sale la bala
    public BulletMovement bulletPrefab;             //Script del movimiento de la bala
    public bool disarmable = true;                  //Si puede ser desarmado
    public float disarmCoolodown = 3;

    protected float shootCooldown = 0f;             //Tiempo hasta la siguiente bala
    protected Transform player { get { return GameManager.instance.player.transform; } }
    protected Transform bulletPool { get { return GameManager.instance.bulletPool; } }                 //Padre de las balas
    protected bool disarmed = false;
    protected float disarmTimer = 0f;
    protected float disCooldown = 0f;

    public virtual void Update()
    {
        Cooldown();
    }

    /// <summary>
    /// Reduce shootCooldown cada segundo
    /// </summary>
    public virtual void Cooldown()
    {
        if (!disarmed)
        {
            if (shootCooldown > 0f) shootCooldown -= Time.deltaTime;        //Se reduce si no es 0
            else shootCooldown = 0f;
        }

        if (disarmTimer > 0f) disarmTimer -= Time.deltaTime;
        else if (disarmTimer < 0f) disarmTimer = 0f;
        else if (disarmed) Rearm();

        if (disCooldown > 0f) disarmTimer -= Time.deltaTime;
        else if (disCooldown < 0f) disarmTimer = 0f;
    }

    /// <summary>
    /// Genera una bala y establece el enfriamiento.
    /// </summary>
    public virtual void Shoot()
    {
        if (shootCooldown == 0f && !disarmed)   //Si puede disparar
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

    public void Disarm(float duration)
    {
        if (disarmable && disCooldown == 0f && disarmTimer == 0f)
        {
            disarmed = true;
            disarmTimer = duration;
        }
    }

    public void Rearm()
    {
        disCooldown = disarmCoolodown;
        disarmed = false;
    }
}
