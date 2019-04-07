using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject player;
    public UIManager ui;
    public Transform bulletPool;
    public string activeScene { get { return SceneManager.GetActiveScene().name; } }

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

    /// <summary>
    /// Recarga la escena
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(activeScene);
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
        SceneManager.LoadScene("Nivel 1");
        ItemManager.instance.DeleteItems();
    }
    public void CargaNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }
    public void CerrarJuego()
    {
        Debug.Log("Juego Cerrado");
        Application.Quit();
    }
}
