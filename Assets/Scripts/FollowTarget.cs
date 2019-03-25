using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform target;

    /// <summary>
    /// Hace que la posición sea la del target excepto la z.
    /// </summary>
    private void LateUpdate()
    {
        Vector3 pos = target.transform.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
