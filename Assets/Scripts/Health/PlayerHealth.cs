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
    public AudioClip damageClip, healClip, deathClip;

    AudioSource audioSource;
    private float invulnerability;
    private Animator anim { get { return GameManager.instance.player.GetComponent<Animator>(); } }
    int curHealth, curMaxHealth;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            curHealth = baseMaxHealth;
            curMaxHealth = baseMaxHealth;

            GameManager.instance.onPlayerTookDamage += TakeDamage;
            GameManager.instance.onPlayerRestoredHealth += RestoreHealth;
        }
        else Destroy(gameObject);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        curMaxHealth = baseMaxHealth;
        if (GameManager.instance.ui != null)
            GameManager.instance.ui.UpdateLives(curHealth, curMaxHealth);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.ui.UpdateLives(curHealth, curMaxHealth);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Baja el tiempo de invulnerabilidad
    /// </summary>
    private void Update()
    {
        if (GameManager.instance.player != null)
        {
            if (invulnerability > 0f)
            {
                invulnerability -= Time.deltaTime;
            }
            else if (invulnerability < 0f)
            {
                invulnerability = 0f;
                anim.SetLayerWeight(1, 0);
            }
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
        if (curHealth > curMaxHealth) curHealth = curMaxHealth;

        GameManager.instance.ui.UpdateLives(curHealth, curMaxHealth);
        audioSource.PlayOneShot(healClip);
    }

    /// <summary>
    /// Añade amount de vida máxima, hasta absoluteMaxHealth, o hasta absoluteMinHealth (se muere)
    /// </summary>
    public void AddMaxHealth(int amount)
    {
        curMaxHealth += amount;
        if (curHealth > curMaxHealth)
            curHealth = curMaxHealth;
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
}
