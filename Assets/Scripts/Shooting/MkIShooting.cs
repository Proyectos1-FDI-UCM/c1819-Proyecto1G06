using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MkIShooting : Shooting {

    public Transform body;

    Animator anim;
    AudioSource audioSource;
    public AudioClip shootClip;
    protected bool shooting = false;    //Indica si está disparando, evita que se activa varias veces el trigger
    protected SpriteRenderer sprite;

    public override void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        base.Awake();
    }

    public virtual void Start()
    {
        ResetCooldown();
    }

    /// <summary>
    /// Apunta al jugador y le dispara
    /// </summary>  
    public override void Update()
    {
        if (controller.GetPlayerDetected())
        {
            Vector2 lookDirection = player.position - transform.position;
            float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI) + (lookDirection.x < 0f ? 180f : 0f);

            Vector2 bodyLookDir = player.position - body.position;
            float bodyAngle = Mathf.Atan(bodyLookDir.y / bodyLookDir.x) * (180 / Mathf.PI) + (bodyLookDir.x < 0f ? 180f : 0f);

            if (bodyAngle > 90 || bodyAngle < -90)
            {
                // Hacer que no tenga un movimiento poco natural
                transform.localScale = new Vector3(-1, 1, 1);
                body.localScale = new Vector3(1, 1, 1);
                sprite.flipY = true;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                body.localScale = new Vector3(-1, 1, 1);
                sprite.flipY = false;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    /// <summary>
    /// Comienza a disparar una bala
    /// </summary>
    public override void Shoot()
    {
        if (shooting == false && shootCooldown == 0 && !disarmed)
        {
            anim.SetTrigger("Shoot");         
            shooting = true;
            audioSource.PlayOneShot(shootClip);
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
