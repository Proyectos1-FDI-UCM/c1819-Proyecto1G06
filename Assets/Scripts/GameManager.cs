using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void OnEffectChanged(BulletEffects effect, ItemData data, Sprite effectSprite);
    public delegate void OnWeaponChanged(Weapons weaponNew, ItemData data, Sprite weaponSprite);
    public delegate void OnPlayerTookDamage();
    public delegate void OnPlayerRestoredHealth(int amount);
    public delegate void OnPlayerAddedDamage(float amount);
    public delegate void OnPlayerAddedSpeed(float amount);
    public delegate void GoingToLoadScene();

    public static GameManager instance;
    public GameObject player;
    public UIManager ui;
    public Transform bulletPool;
    public string activeScene { get { return SceneManager.GetActiveScene().name; } }
    public List<ItemData> spawnedItems;
    int _playerCurHealth = 0;
    public int playerCurHealth { get { return _playerCurHealth; } set { _playerCurHealth = value; } }

    public OnEffectChanged onEffectChanged;
    public OnWeaponChanged onWeaponChanged;
    public OnPlayerTookDamage onPlayerTookDamage;
    public OnPlayerRestoredHealth onPlayerRestoredHealth;
    public OnPlayerAddedDamage onPlayerAddedDamage;
    public OnPlayerAddedSpeed onPlayerAddedSpeed;
    public GoingToLoadScene goingToLoadScene;

    /// <summary>
    /// Singleton
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            spawnedItems = new List<ItemData>();
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            LoadMenu();
        }
    }

    /// <summary>
    /// Recarga la escena
    /// </summary>
    public void ReloadScene()
    {
        LoadScene(activeScene);
    }

    /// <summary>
    /// Carga la escena scene
    /// Borra los métodos de los delegados para que se añadan en la escena
    /// </summary>
    public void LoadScene(string scene)
    {
        if (goingToLoadScene != null)
            goingToLoadScene();
        SceneManager.LoadScene(scene);
    }

    public void InitCurHealth(int amount)
    {
        if(playerCurHealth <= 0)
        {
            playerCurHealth = amount;
        }
    }

    public void PlayerDied()
    {
        LoadMenu();
    }

    public void LoadMenu()
    {
        ItemManager.instance.DeleteItems();
        if(player != null) playerCurHealth = player.GetComponent<PlayerHealth>().baseMaxHealth;
        spawnedItems = new List<ItemData>();
        LoadScene("Menu");
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (ItemManager.instance != null) ItemManager.instance.OnSceneLoaded();
        if (player != null) player.GetComponent<PlayerHealth>().OnSceneLoaded(playerCurHealth);
    }
}
