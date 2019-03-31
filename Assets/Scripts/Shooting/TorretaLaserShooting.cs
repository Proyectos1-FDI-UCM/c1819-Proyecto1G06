using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserShooting : Shooting {

    GameObject player;
    TorretaLaserController laserController;

    private void Awake()
    {
        player = GameManager.instance.player;
        laserController = GetComponentInParent<TorretaLaserController>();
        
    }

    // Update is called once per frame
     public override void Update () {

        if (shootCooldown == 0)
        {
            laserController.ShootLaser();
            ResetCooldown();
            Vector2 lookDirection = (player.transform.position) - transform.position;
            float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI);

            transform.eulerAngles = new Vector3(0, 0, angle + (lookDirection.x < 0f ? 180f : 0f));        
        }

        else
            Cooldown();
     }
}
