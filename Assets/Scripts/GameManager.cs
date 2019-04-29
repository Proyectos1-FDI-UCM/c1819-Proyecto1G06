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
    public delegate void GoingToLoadScene();

    public static GameManager instance;
    public GameObject player;
    public UIManager ui;
    public Transform bulletPool;
    public string activeScene { get { return SceneManager.GetActiveScene().name; } }
    public List<GameObject> spawnedItems;

    public OnEffectChanged onEffectChanged;
    public OnWeaponChanged onWeaponChanged;
    public OnPlayerTookDamage onPlayerTookDamage;
    public OnPlayerRestoredHealth onPlayerRestoredHealth;
    public OnPlayerAddedDamage onPlayerAddedDamage;
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
            SceneManager.sceneLoaded += OnSceneloaded;
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
        PlayerHealth.instance.OnSceneLoaded();
        ItemManager.instance.OnSceneLoaded();
    }
}
