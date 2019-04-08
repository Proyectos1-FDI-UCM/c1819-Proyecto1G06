using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronBlindadoHealth : EnemyHealth
{
    public override void Die()
    {
        Destroy(transform.parent.gameObject);
        SendMessageUpwards("EnemyDied", transform.parent, SendMessageOptions.DontRequireReceiver);
    }
}
