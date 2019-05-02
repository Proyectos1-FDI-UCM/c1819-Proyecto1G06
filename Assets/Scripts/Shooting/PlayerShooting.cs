using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Shooting {

    public float baseDamage = 2;
    public float absoluteMinBaseDamage = 0.5f;
    public float damageMultiplier = 1;
    public BulletMovement[] possibleBullets;
    public SpriteRenderer body;
    public SpriteRenderer effectArm;
    public AudioClip shootClip;
    //Weapons weapon; //Arma, de momento sin usar

    SpriteRenderer arm;
    AudioSource audioSource;

    private void Awake()
    {
        GameManager.instance.onEffectChanged += ChangeEffect;
        GameManager.instance.onWeaponChanged += ChangeWeapon;
        GameManager.instance.onPlayerAddedDamage += AddDamage;
        GameManager.instance.goingToLoadScene += DeleteDelegatesShooting;
        arm = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        GameManager.instance.ui.UpdateDamage(baseDamage * damageMultiplier, damageMultiplier);       
        //weapon = Weapons.Default;
    }

    /// <summary>
    /// Apunta hacia el ratón y dispara al hacer Fire1
    /// </summary>
    public override void Update()
    {
        Vector2 lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;   //Vector entre el ratón y el jugador

        //Arctan se queda con el ángulo más pequeño y utilizamos un offset para corregirlo
        float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI) + (lookDirection.x < 0f ? 180f : 0f);    
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (angle > 90 || angle < -90)
        {
            GetComponent<SpriteRenderer>().flipY = true; // Hacer que no tenga un movimiento poco natural
            effectArm.flipY = true;
            body.flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
            effectArm.flipY = false;
            body.flipX = false;
        }

        if (Input.GetButton("Fire1") && shootCooldown == 0f)        //Dispara al hacer clic
            Shoot();

        // Se comprueba el shootCooldown a la hora de disparar
        base.Update();
    }

    /// <summary>
    /// Dispara una bala
    /// </summary>
    public override void Shoot()
    {
        ResetCooldown();
        BulletMovement newBullet = Instantiate<BulletMovement>(bulletPrefab, shootingPoint.position, Quaternion.identity, bulletPool);
        newBullet.GetComponent<PlayerBullet>().Damage = baseDamage * damageMultiplier;
        newBullet.Rotate(transform.right);
        audioSource.PlayOneShot(shootClip);
    }

    /// <summary>
    /// Añade amount de daño base
    /// </summary>
    public void AddDamage(float amount)
    {
        baseDamage += amount;
        if (baseDamage < absoluteMinBaseDamage) baseDamage = absoluteMinBaseDamage;
        GameManager.instance.ui.UpdateDamage(baseDamage * damageMultiplier, damageMultiplier);
        GameManager.instance.ui.TextAddDmg(damageMultiplier * amount);
    }

    /// <summary>
    /// Cambia la bala según el nuevo efecto.
    /// </summary>
    public void ChangeEffect(BulletEffects effect, ItemData data, Sprite effectSprite)
    {
        if(ItemManager.instance.CurrentEfect() != BulletEffects.Illegal)
        {
            bulletPrefab = possibleBullets[(int)effect];
            effectArm.sprite = effectSprite;
        }
    }

    public void ChangeWeapon(Weapons weaponNew, ItemData data, Sprite weaponSprite)
    {
        UIManager ui = GameManager.instance.ui;
        switch (weaponNew)
        {
            case (Weapons.Default):
                rateOfFire = 2f;
                damageMultiplier = 1f;
                break;
            case (Weapons.LenteConvergente):
                rateOfFire = 0.75f;
                damageMultiplier = 2.5f;
                break;
            case (Weapons.ReactorCinético):
                rateOfFire = 5f;
                damageMultiplier = 0.5f;
                break;
        }

        ui.UpdateDamage(baseDamage * damageMultiplier, damageMultiplier);
        arm.sprite = weaponSprite;

        //weapon = weaponNew;
    }

    public void DeleteDelegatesShooting()
    {
        GameManager.instance.onEffectChanged -= ChangeEffect;
        GameManager.instance.onWeaponChanged -= ChangeWeapon;
        GameManager.instance.onPlayerAddedDamage -= AddDamage;
        GameManager.instance.goingToLoadScene -= DeleteDelegatesShooting;
    }
}
