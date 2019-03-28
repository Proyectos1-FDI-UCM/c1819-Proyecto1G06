using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MkIShooting : Shooting {

    Animator anim;
    protected bool shooting = false;    //Indica si está disparando, evita que se activa varias veces el trigger

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Start()
    {
        ResetCooldown();
    }

    /// <summary>
    /// Apunta al jugador y le dispara
    /// </summary>  
    public override void Update()
    {
        Vector2 lookDirection = player.position - transform.position;
        float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI);

        transform.eulerAngles = new Vector3(0, 0, angle + (lookDirection.x < 0f ? 180f : 0f));
    }

    /// <summary>
    /// Comienza a disparar una bala
    /// </summary>
    public override void Shoot()
    {
        if (shooting == false && shootCooldown == 0)
        {
            anim.SetTrigger("Shoot");
            shooting = true;
        }
    }

    /// <summary>
    /// Dispara una bala
    /// </summary>
    public virtual void Shot()
    {
        ResetCooldown();
        BulletMovement newBullet = Instantiate<BulletMovement>(bulletPrefab, shootingPoint.position, Quaternion.identity, bulletPool);
        newBullet.Rotate(transform.right);
        shooting = false;
    }

    public bool GetShooting()
    {
        return shooting;
    }
}
