using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour {

    public RoomManager room;

    /// <summary>
    /// Detecta al jugador
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            room.DetectPlayer();
        }
    }
}
