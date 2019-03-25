using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public Transform enemies;
    public GameObject doors;
    public GameObject itemPos;
    public Coord pos;
    public bool boss = false;

    protected RoomState state = RoomState.NonVisited;       // Estado de la sala

    public virtual void Start()
    {
        enemies.gameObject.SetActive(false);
        doors.SetActive(false);
        itemPos.SetActive(false);
        Minimap.instance.StoreRoom(pos, boss);  //Inicializa esta habitación
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
        Minimap.instance.NewRoomExplored(pos);
        if (enemies.childCount > 0)
        {
            state = RoomState.Closed;
            enemies.gameObject.SetActive(true);
            ToggleDoors(state);
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
    }

    /// <summary>
    /// Cambia los items según el estado de la habitación
    /// </summary>
    protected void ToggleItems(RoomState state)
    {
        itemPos.SetActive(state == RoomState.Open ? true : false);
    }
}
