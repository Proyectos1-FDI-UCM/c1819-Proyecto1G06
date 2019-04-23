using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaArtilleríaShooting : Shooting {

    public SpriteRenderer neck;

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
        float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI) + (lookDirection.x < 0f ? 180f : 0f);

        if (angle > 90 || angle < -90)  // Hacer que no tenga un movimiento poco natural
        {
            GetComponent<SpriteRenderer>().flipY = true;
            neck.flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
            neck.flipX = true;
        }
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
