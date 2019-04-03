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
            anim.SetBool("Invulnerable", false);
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
            anim.SetBool("Invulnerable", true);
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
    /// <param name="heal">Si además debería curarse el jugador</param>
    public void AddMaxHealth(int amount, bool heal)
    {
        maxHealth += amount;
        if (maxHealth > absoluteMaxHealth)
            maxHealth = absoluteMaxHealth;
        if (heal) RestoreHealth(amount);

        ui.UpdateLives(curHealth, maxHealth);
    }
}
