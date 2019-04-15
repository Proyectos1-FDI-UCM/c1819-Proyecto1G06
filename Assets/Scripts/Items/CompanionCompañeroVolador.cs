using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCompañeroVolador : MonoBehaviour {

    [Tooltip("La distancia mínima a la que tiene que estar para que se empiece a mover")]public float minDistance = 1;

    FollowDirection follow;

    void Start()
    {
        follow = GetComponent<FollowDirection>();
    }

    private void Update()
    {
        Vector3 dir = GameManager.instance.player.transform.position - transform.position;
        if (dir.magnitude > minDistance)
            follow.MoveTowards(dir);
    }
}
