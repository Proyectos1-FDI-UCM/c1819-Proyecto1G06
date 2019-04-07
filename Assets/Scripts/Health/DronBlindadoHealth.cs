using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronBlindadoHealth : EnemyHealth
{
    GameObject parent;
    public override void Die()
    {
        SendMessageUpwards("EnemyDied", transform, SendMessageOptions.DontRequireReceiver);
        parent = GetComponentInParent<Transform>().gameObject;
        Destroy(parent);
    }
}
