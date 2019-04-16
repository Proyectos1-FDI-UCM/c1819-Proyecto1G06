﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDamageWithMissingHealth : MonoBehaviour {

    public float damageDivider = 2f;

    float curDamageBonus = 0;

    /// <summary>
    /// Añade un listener a cuando el jugador cambie su vida
    /// </summary>
    public void PickEffect()
    {
        GameManager.instance.onPlayerTookDamage += PlayerChangedHealth;
        PlayerChangedHealth();
    } 

    void PlayerChangedHealth()
    {
        PlayerHealth playerHealth = GameManager.instance.player.GetComponent<PlayerHealth>();
        GameManager.instance.onPlayerAddedDamage(-curDamageBonus);
        curDamageBonus = (playerHealth.maxHealth - playerHealth.CurrentHealth()) / damageDivider;
        GameManager.instance.onPlayerAddedDamage(curDamageBonus);
    }
}
