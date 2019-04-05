using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPointController : MonoBehaviour {

    Transform player;
    FollowDirection follow;

    private void Start()
    {
        player = GameManager.instance.player.transform;
        follow = GetComponent<FollowDirection>();
    }

    private void Update()
    {
        follow.MoveTowards(player.position - transform.position);
    }
}
