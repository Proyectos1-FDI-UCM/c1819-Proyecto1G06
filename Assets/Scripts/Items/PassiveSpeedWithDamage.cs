using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpeedWithDamage : MonoBehaviour, IItem {

    [Range(0f, 1f)] public float percentageConversion;

    float currSpeed = 0;

	public void PickEffect()
    {
        GameManager.instance.onPlayerAddedDamage += AddDamageToSpeed;
        AddDamageToSpeed(0f);
    }

    public void AddDamageToSpeed(float damage)
    {
        GameManager.instance.player.GetComponent<PlayerMovement>().AddSpeed(- currSpeed);
        currSpeed = GameManager.instance.player.GetComponentInChildren<PlayerShooting>().baseDamage * percentageConversion;
        GameManager.instance.player.GetComponent<PlayerMovement>().AddSpeed(currSpeed);
    }
}
