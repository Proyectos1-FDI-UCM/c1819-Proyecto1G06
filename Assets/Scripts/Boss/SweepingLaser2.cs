using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingLaser2 : SweepingLaser {

	public override void Update ()
    {
        lr.SetPosition(0, shootingPoint.position);
        lr.SetPosition(1, followPoint.position);
        base.Update();
    }
}
