using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth instance;
    public int baseMaxHealth = 4;
    public int absoluteMaxHealth = 10;
    public int absoluteminHealth = 0;
    public float invulTime = 1f;
    public float invulnerability { get { return _invulnerability; } }

    private float _invulnerability;
    public AudioClip damageClip, healClip, deathClip;

    AudioSource audioSource;
    private Animator anim { get { return GameManager.instance.player.GetComponent<Animator>(); } }
    int curHealth, curMaxHealth;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ResetHealth();

            GameManager.instance.onPlayerTookDamage += TakeDamage;
            GameManager.instance.onPlayerRestoredHealth += RestoreHealth;
        }
        else Destroy(gameObject);
    }

    public void OnSceneLoaded()
    {
        curHealth = curHealth - (curMaxHealth - baseMaxHealth);
        curMaxHealth = baseMaxHealth;
        if (GameManager.instance.ui != null)
            GameManager.instance.ui.UpdateLives(curHealth, curMaxHealth);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.ui.UpdateLives(curHealth, curMaxHealth);
    }

    /// <summary>
    /// Baja el tiempo de invulnerabilidad
    /// </summary>
    private void Update()
    {
        if (GameManager.instance.player != null)
        {
            if (_invulnerability > 0f)
            {
                _invulnerability -= Time.deltaTime;
            }
            else if (_invulnerability < 0f)
            {
                _invulnerability = 0f;
                anim.SetLayerWeight(1, 0);
            }
        }
    }

    /// <summary>
    /// Se reduce la vida, si es cero, muere
    /// </summary>
    public void TakeDamage()
    {
        if (_invulnerability == 0f)  //Mientras no sea invulnerable
        {
            _invulnerability = invulTime;    //Le hace invulnerable
            anim.SetLayerWeight(1, 1);
            curHealth--;
            GameManager.instance.ui.UpdateLives(curHealth, curMaxHealth);      //Hacer que el UIManager actualice la UI
            audioSource.PlayOneShot(damageClip);

            if (curHealth <= 0)
            {
                curHealth = baseMaxHealth;
                GameManager.instance.PlayerDied();
                audioSource.PlayOneShot(deathClip);
            }
        }
    }

    /// <summary>
    /// Aumenta la vida en amount, hasta el máximo.
    /// </summary>
    public void RestoreHealth(int amount)
    {
        curHealth += amount;
        if (curHealth > curMaxHealth)curHealth = curMaxHealth;

        else audioSource.PlayOneShot(healClip);

        GameManager.instance.ui.UpdateLives(curHealth, curMaxHealth);

        if (curHealth <= 0)
        {
            curHealth = baseMaxHealth;
            GameManager.instance.PlayerDied();
        }
    }

    /// <summary>
    /// Añade amount de vida máxima, hasta absoluteMaxHealth, o hasta absoluteMinHealth (se muere)
    /// </summary>
    public void AddMaxHealth(int amount)
    {
        curMaxHealth += amount;
        if (curMaxHealth > absoluteMaxHealth)
            curMaxHealth = absoluteMaxHealth;
        else if (curMaxHealth <= absoluteminHealth)
        {
            curMaxHealth = absoluteminHealth;
            GameManager.instance.PlayerDied();
        }
        GameManager.instance.ui.UpdateLives(curHealth, curMaxHealth);
    }

    public int CurrentHealth()
    {
        return curHealth;
    }

    public int CurrentMaxHealth()
    {
        return curMaxHealth;
    }

    /// <summary>
    /// Pone la vida máxima actual a la base y recupera toda la vida al jugador
    /// </summary>
    public void ResetHealth()
    {
        curHealth = baseMaxHealth;
        curMaxHealth = baseMaxHealth;
    }
}
