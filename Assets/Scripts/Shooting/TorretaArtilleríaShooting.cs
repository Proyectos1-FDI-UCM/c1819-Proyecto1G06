using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaArtilleríaShooting : Shooting {

    public void Start()
    {
        ResetCooldown();
    }

    /// <summary>
    /// Apunta al jugador y le dispara
    /// </summary>  
    public override void Update ()
    {
        Vector2 lookDirection = (player.position) - transform.position;
        float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI);

        transform.eulerAngles = new Vector3(0, 0, angle + (lookDirection.x < 0f ? 180f : 0f));
    }
}
