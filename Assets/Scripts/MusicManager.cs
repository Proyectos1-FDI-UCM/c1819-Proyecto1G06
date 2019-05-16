using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip intro;
    public AudioClip battle;
    public AudioClip vsIA;
    AudioSource musica;
    public static MusicManager instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musica = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);

    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
        case "Nivel 1":
        case "Nivel 2":
                if (musica.clip != battle) //Si esta ejecutando el audio battle, se evita que se reinicie el audio
                {
                    musica.clip = battle;
                    musica.Play();
                }
        break;
        
        case "Nivel IA":
                musica.clip = vsIA;
                musica.Play();
        break;

        case "Menu":
        case "ModoDios":
                if (musica.clip != intro)
                {
                    musica.clip = intro;
                    musica.Play();
                }
        break;

            
        }
    }
}
