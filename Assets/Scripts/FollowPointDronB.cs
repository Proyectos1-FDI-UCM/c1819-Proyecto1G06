using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPointDronB : MonoBehaviour {


    public Transform Amigo;
    public Transform player;
    RaycastHit2D hit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        hit = Physics2D.Linecast(Amigo.position, player.position);
        Debug.DrawLine(player.position, hit.transform.position);

        transform.position = hit.transform.position / 2;
    }
}
