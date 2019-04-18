using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCompañeroMímico : CompanionCompañeroDeAtaque
{
    PlayerShooting playerShooting;

    private void Awake()
    {
        playerShooting = GameManager.instance.player.GetComponentInChildren<PlayerShooting>();
        GameManager.instance.onEffectChanged += ChangeEffect;
        GameManager.instance.onWeaponChanged += ChangeWeapon;
        GameManager.instance.onPlayerAddedDamage += AddDamage;
        GameManager.instance.goingToLoadScene += DeleteDelegatesCM;
    }

    private void Start()
    {
        damage = playerShooting.damageMultiplier * playerShooting.baseDamage;
        rateOfFire = playerShooting.rateOfFire;
        bulletPrefab = playerShooting.bulletPrefab;
    }

    void ChangeEffect(BulletEffects effect, ItemData data, Sprite effectSprite)
    {
        bulletPrefab = playerShooting.bulletPrefab;
    }

    private void ChangeWeapon(Weapons weaponNew, ItemData data, Sprite weaponSprite)
    {
        damage = playerShooting.damageMultiplier * playerShooting.baseDamage;
        rateOfFire = playerShooting.rateOfFire;
    }

    void AddDamage(float amount)
    {
        damage = playerShooting.baseDamage * playerShooting.damageMultiplier;
    }

    public void DeleteDelegatesCM()
    {
        GameManager.instance.onEffectChanged -= ChangeEffect;
        GameManager.instance.onWeaponChanged -= ChangeWeapon;
        GameManager.instance.onPlayerAddedDamage -= AddDamage;
        GameManager.instance.goingToLoadScene -= DeleteDelegatesCM;
    }
}
