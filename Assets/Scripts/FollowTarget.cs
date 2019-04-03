using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform target;
    public bool followX = true;
    public bool followY = true;

    /// <summary>
    /// Hace que la posición sea la del target excepto la z.
    /// </summary>
    private void LateUpdate()
    {
        Vector3 pos = target.transform.position;

        if (followX) pos.x = target.transform.position.x;
        else pos.x = 0;

        if (followY) pos.y = target.transform.position.y;
        else pos.y = 0;

        pos.z = transform.position.z;
        transform.position = pos;
    }
}
