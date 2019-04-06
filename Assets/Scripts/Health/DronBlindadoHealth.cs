using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronBlindadoHealth : EnemyHealth
{
    public override void Die()
    {
        SendMessageUpwards("EnemyDied", transform, SendMessageOptions.DontRequireReceiver);
        Destroy(transform.parent.gameObject);
    }
}
