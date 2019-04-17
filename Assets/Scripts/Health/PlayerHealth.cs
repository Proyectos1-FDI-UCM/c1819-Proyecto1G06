using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth instance;
    public int maxHealth = 4;
    public int absoluteMaxHealth = 10;
    public float invulTime = 1f;

    private float invulnerability;
    private UIManager ui { get { return GameManager.instance.ui; } }
    private Animator anim { get { return GameManager.instance.player.GetComponent<Animator>(); } }
    int curHealth;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        curHealth = maxHealth;

        GameManager.instance.onPlayerTookDamage += TakeDamage;
        GameManager.instance.onPlayerRestoredHealth += RestoreHealth;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (ui != null)
            ui.UpdateLives(curHealth, maxHealth);
    }

    void Start()
    {
        ui.UpdateLives(curHealth, maxHealth);
        SceneManager.sceneLoaded += OnSceneLoaded;
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

        print(curHealth);
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
                curHealth = maxHealth;
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
