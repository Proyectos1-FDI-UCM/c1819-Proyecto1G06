using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int maxHealth = 3;
    public int absoluteMaxHealth = 10;
    public float invulTime = 1f;

    private float invulnerability;
    private UIManager ui { get { return GameManager.instance.ui; } }
    private Animator anim;
    int curHealth;

    private void Awake()
    {
        curHealth = maxHealth;
        anim = GetComponent<Animator>();
        GameManager.instance.player = gameObject;

        GameManager.instance.onPlayerTookDamage += TakeDamage;
        GameManager.instance.onPlayerRestoredHealth += RestoreHealth;
    }

    /// <summary>
    /// Vida inicial
    /// </summary>
    private void Start()
    {
        ui.UpdateLives(curHealth, maxHealth);
    }


    /// <summary>
    /// Baja el tiempo de invulnerabilidad
    /// </summary>
    private void Update()
    {
        if(invulnerability > 0f)
        {
            invulnerability -= Time.deltaTime;
        }
        else if (invulnerability < 0f)
        {
            invulnerability = 0f;
            anim.SetLayerWeight(1, 0);
        }
    }

    /// <summary>
    /// Se reduce la vida, si es cero, muere
    /// </summary>
    public void TakeDamage()
    {
        if (invulnerability == 0f)  //Mientras no sea invulnerable
        {
            invulnerability = invulTime;    //Le hace invulnerable
            anim.SetLayerWeight(1, 1);
            curHealth--;
            ui.UpdateLives(curHealth, maxHealth);      //Hacer que el UIManager actualice la UI

            if (curHealth <= 0)
            {
                curHealth = 0;
                GameManager.instance.PlayerDied();
            }
        }
       
    }

    /// <summary>
    /// Aumenta la vida en amount, hasta el máximo.
    /// </summary>
    public void RestoreHealth(int amount)
    {
        curHealth += amount;
        if (curHealth > maxHealth) curHealth = maxHealth;

        ui.UpdateLives(curHealth, maxHealth);
    }

    /// <summary>
    /// Añade amount de vida máxima, hasta absoluteMaxHealth
    /// </summary>
    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
        if (maxHealth > absoluteMaxHealth)
            maxHealth = absoluteMaxHealth;

        ui.UpdateLives(curHealth, maxHealth);
    }

    public int CurrentHealth()
    {
        return curHealth;
    }
}
