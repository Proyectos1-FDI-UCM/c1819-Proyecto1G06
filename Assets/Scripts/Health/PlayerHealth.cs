using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {


    public int baseMaxHealth = 4;
    public int absoluteMaxHealth = 10;
    public int absoluteminHealth = 0;
    public float invulTime = 1f;
    public float invulnerability { get { return _invulnerability; } }

    private float _invulnerability;
    public AudioClip damageClip, healClip, deathClip;

    AudioSource audioSource;
    private Animator anim;
    int curHealth, curMaxHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        GameManager.instance.InitCurHealth(baseMaxHealth);
        curHealth = baseMaxHealth;
        curMaxHealth = baseMaxHealth;

        GameManager.instance.onPlayerTookDamage += TakeDamage;
        GameManager.instance.onPlayerRestoredHealth += RestoreHealth;
        GameManager.instance.goingToLoadScene += GoingToLoadScene;
    }

    public void OnSceneLoaded(int curHealth)
    {
        this.curHealth = curHealth;
    }

    public void GoingToLoadScene()
    {
        GameManager.instance.playerCurHealth = curHealth;
        GameManager.instance.onPlayerTookDamage -= TakeDamage;
        GameManager.instance.onPlayerRestoredHealth -= RestoreHealth;
        GameManager.instance.goingToLoadScene -= GoingToLoadScene;
    }

    void Start()
    {
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
                this.gameObject.layer = LayerMask.NameToLayer("Player");
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
            this.gameObject.layer = LayerMask.NameToLayer("PlayerInmune");
            if (curHealth <= 0)
            {
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
            GameManager.instance.PlayerDied();
            audioSource.PlayOneShot(deathClip);
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
