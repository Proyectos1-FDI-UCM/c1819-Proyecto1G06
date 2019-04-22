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
            state = RoomState.Closed;
            enemies.gameObject.SetActive(true);
            GameManager.instance.ui.ToggleBossHealth(true);
            ToggleDoors(state);
            cameraPlayer.SetActive(false);
            cameraBoss.SetActive(true);
        }
    }

}
