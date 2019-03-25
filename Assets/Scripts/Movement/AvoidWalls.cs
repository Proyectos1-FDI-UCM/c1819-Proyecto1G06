using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidWalls : MonoBehaviour {
    //WIP, NO FUNCIONAL
    //POSIBLEMENTE RELEGADO POR NAVMESH
    public float minDistance = 3f;

    Collider2D col;
    RaycastHit2D[] sight = new RaycastHit2D[1];
    FollowDirection follow;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public bool HitWall(Vector2 direction, out Vector2 normal)
    {
        col.Raycast(direction, sight, minDistance, LayerMask.GetMask("Player", "Environment"));
        normal = sight[0].normal;
        return (sight[0].collider != null && sight[0].transform.gameObject.layer == LayerMask.NameToLayer("Environment"));
    }
}
