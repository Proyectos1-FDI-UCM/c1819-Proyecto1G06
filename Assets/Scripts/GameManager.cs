using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void OnEffectChanged(BulletEffects effect, ItemData data, Sprite effectSprite);
    public delegate void OnWeaponChanged(Weapons weaponNew, ItemData data, Sprite weaponSprite);

    public static GameManager instance;

    public GameObject player;
    public UIManager ui;
    public Transform bulletPool;
    public string activeScene { get { return SceneManager.GetActiveScene().name; } }

    public OnEffectChanged onEffectChanged;
    public OnWeaponChanged onWeaponChanged;

    /// <summary>
    /// Singleton
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
    /// </summary>
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void PlayerDied()
    {
        LoadMenu();
    }

    void LoadMenu()
    {
        ItemManager.instance.DeleteItems();
        LoadScene("Menu");
    }
}
