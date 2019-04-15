using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : RoomManager {

    public GameObject endTrigger;

    /// <summary>
    /// Desactiva la salida del nivel
    /// </summary>
    public override void Start()
    {
        endTrigger.SetActive(false);
        base.Start();
    }

    /// <summary>
    /// Desactiva puertas y activa items y salida del nivel al morir los enemigos.
    /// </summary>
    /// <param name="sender">El enemigo que ha muerto</param>
    public override void EnemyDied(Transform sender)
    {
        sender.transform.parent = null; // Elimina el enemigo de los hijos para que no cuente

        if (enemies.childCount == 0)
        {
            state = RoomState.Open;
            ToggleDoors(state);
            endTrigger.SetActive(true);
            itemPos.SetActive(true);
            GameManager.instance.ui.ToggleBossHealth(false);
        }
    }

    /// <summary>
    /// Actualiza el mapa, y, si hay enemigos, cierra la habitación
    /// </summary>
    public override void DetectPlayer()
    {
        Minimap.instance.NewRoomExplored(pos);
        if (enemies.childCount > 0 && state != RoomState.Closed)
        {
            state = RoomState.Closed;
            SpawnIndicators();
            Invoke("SummonEnemies", summonTime);
            ToggleDoors(state);
            GameManager.instance.ui.ToggleBossHealth(true);
            audioSource.PlayOneShot(spawnClip);
        }
    }
}
