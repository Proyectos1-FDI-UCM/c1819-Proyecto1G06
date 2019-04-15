using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinalManager : RoomManager {

    public GameObject cameraPlayer;
    public GameObject cameraBoss;

    public override void DetectPlayer()
    {
        if (state != RoomState.Closed)
        {
            Minimap.instance.NewRoomExplored(pos);
            state = RoomState.Closed;
            enemies.gameObject.SetActive(true);
            ToggleDoors(state);
            cameraPlayer.SetActive(false);
            cameraBoss.SetActive(true);
        }
    }

}
