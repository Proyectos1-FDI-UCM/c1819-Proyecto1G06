using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDamageWithMissingHealth : MonoBehaviour, IItem {

    public float damageDivider = 2f;

    float curDamageBonus = 0;

    /// <summary>
    /// Añade un listener a cuando el jugador cambie su vida
    /// </summary>
    public void PickEffect()
    {
        GameManager.instance.onPlayerTookDamage += PlayerChangedHealth;
        GameManager.instance.onPlayerRestoredHealth += PlayerChangedHealth;
        GameManager.instance.goingToLoadScene += DeleteDelegatesSR;
        curDamageBonus = 0;
        PlayerChangedHealth();
    } 

    void PlayerChangedHealth()
    {
        GameManager.instance.onPlayerAddedDamage(-curDamageBonus);
        curDamageBonus = (PlayerHealth.instance.baseMaxHealth - PlayerHealth.instance.CurrentHealth()) / damageDivider;
        GameManager.instance.onPlayerAddedDamage(curDamageBonus);
    }

    void PlayerChangedHealth(int amount)
    {
        PlayerChangedHealth();
    }

    public void DeleteDelegatesSR()
    {
        GameManager.instance.onPlayerTookDamage -= PlayerChangedHealth;
        GameManager.instance.onPlayerRestoredHealth -= PlayerChangedHealth;
        GameManager.instance.goingToLoadScene -= DeleteDelegatesSR;
    }
}
