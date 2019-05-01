using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpeedWithDamage : MonoBehaviour, IItem {

    [Range(0f, 1f)] public float percentageConversion;

    float currSpeed = 0;

	public void PickEffect()
    {
        GameManager.instance.onPlayerAddedDamage += AddDamageToSpeed;
        GameManager.instance.goingToLoadScene += DeleteDelegatesAF;
        currSpeed = 0;
        AddDamageToSpeed();
    }

    public void AddDamageToSpeed(float damage)
    {
        AddDamageToSpeed();
    }

    public void AddDamageToSpeed()
    {
        if (GameManager.instance.onPlayerAddedSpeed != null)
        {
            GameManager.instance.onPlayerAddedSpeed(-currSpeed);
            currSpeed = GameManager.instance.player.GetComponentInChildren<PlayerShooting>().baseDamage * percentageConversion;
            GameManager.instance.onPlayerAddedSpeed(currSpeed);
        }
    }

    public void DeleteDelegatesAF()
    {
        GameManager.instance.onPlayerAddedDamage -= AddDamageToSpeed;
        GameManager.instance.goingToLoadScene -= DeleteDelegatesAF;
    }
}
