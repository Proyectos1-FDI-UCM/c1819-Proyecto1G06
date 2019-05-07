using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public Transform enemies;
    public GameObject doors;
    public GameObject itemPos;
    public AudioClip doorClip, spawnClip;
    public GameObject enemySpawnIndicator;
    public Vector2Int pos;
    public bool boss = false;

    protected RoomState state = RoomState.NonVisited;       // Estado de la sala
    protected AudioSource audioSource;
    protected float summonTime = 2.2f;
    GameObject[] indicators;

    public void Awake()
    {
        indicators = new GameObject[enemies.childCount];
        int cont = 0;
        for(int i = 0; i < enemies.childCount; i++)
        {
            GameObject indicator = Instantiate<GameObject>(enemySpawnIndicator, enemies.GetChild(i).position, Quaternion.identity, transform);
            indicators[cont] = indicator;
            indicator.SetActive(false);
            cont++;
        }
        enemies.gameObject.SetActive(false);
        doors.SetActive(false);
        itemPos.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Al morir los enemigos, cambia de estado
    /// </summary>
    public virtual void EnemyDied(Transform sender)
    {
        sender.transform.parent = null; // Elimina el enemigo de los hijos para que no cuente

        if (enemies.childCount == 0)
        {
            state = RoomState.Open;
            ToggleItems(state);
            ToggleDoors(state);
        }
    }

    /// <summary>
    /// El jugador está en la sala, actualizar el estado
    /// </summary>
    public virtual void DetectPlayer()
    {
       // Minimap.instance.NewRoomExplored(pos);
        if (enemies.childCount > 0 && state != RoomState.Closed)
        {
            state = RoomState.Closed;
            SpawnIndicators();
            Invoke("SummonEnemies", summonTime);
            ToggleDoors(state);
            audioSource.PlayOneShot(spawnClip);
        }
    }

    protected void SpawnIndicators()
    {
        for(int i = 0; i < indicators.Length; i++)
        {
            indicators[i].SetActive(true);
        }
    }

    /// <summary>
    /// Invoca a los enemigos
    /// </summary>
    private void SummonEnemies()
    {
        enemies.gameObject.SetActive(true);
        for (int i = 0; i < indicators.Length; i++)
        {
            Destroy(indicators[i]);
        }
    }

    /// <summary>
    /// Abre o cierra las puertas dependiendo del estado
    /// </summary>
    protected void ToggleDoors(RoomState state)
    {
        bool toggle = false;
        if (state == RoomState.Closed) toggle = true;
        doors.SetActive(toggle);
        audioSource.PlayOneShot(doorClip);
    }

    /// <summary>
    /// Cambia los items según el estado de la habitación
    /// </summary>
    protected void ToggleItems(RoomState state)
    {
        itemPos.SetActive(state == RoomState.Open ? true : false);
    }
}
