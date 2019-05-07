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

    public OnEffectChanged onEffectChanged;
    public OnWeaponChanged onWeaponChanged;
    public OnPlayerTookDamage onPlayerTookDamage;
    public OnPlayerRestoredHealth onPlayerRestoredHealth;
    public OnPlayerAddedDamage onPlayerAddedDamage;
    public OnPlayerAddedSpeed onPlayerAddedSpeed;
    public GoingToLoadScene goingToLoadScene;

    public GameObject TORv0Items, VoluntarioItems;

    /// <summary>
    /// Singleton
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneloaded;
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

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (TORv0Items == null) TORv0Items = GameObject.Find("TORv0Items");
            TORv0Items.transform.position = new Vector3(4f, 4f);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (VoluntarioItems == null) VoluntarioItems = GameObject.Find("VoluntarioItems");
            VoluntarioItems.transform.position = new Vector3(4f, 0);
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
        if(PlayerHealth.instance != null) PlayerHealth.instance.RestoreHealth(PlayerHealth.instance.CurrentMaxHealth());
        SceneManager.LoadScene(scene);
    }

    public void PlayerDied()
    {
        LoadMenu();
    }

    public void LoadMenu()
    {
        ItemManager.instance.DeleteItems();
        PlayerHealth.instance.ResetHealth();
        LoadScene("Menu");
    }

    public void OnSceneloaded(Scene scene, LoadSceneMode mode)
    {
        if(PlayerHealth.instance != null) PlayerHealth.instance.OnSceneLoaded();
        if(ItemManager.instance != null) ItemManager.instance.OnSceneLoaded();
    }
}
