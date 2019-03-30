using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserShooting : Shooting {


    // Update is called once per frame
    public override void  Update () {
        Vector2 lookDirection = (player.position) - transform.position;
        float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI);

        transform.eulerAngles = new Vector3(0, 0, angle + (lookDirection.x < 0f ? 180f : 0f));
    }
}
