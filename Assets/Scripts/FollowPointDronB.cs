using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPointDronB : MonoBehaviour {


    public Transform Amigo;
    public Transform player;
    Vector2 hit;

	
	void Update () {
        // Creamos un vector entre Jugador y el enemigo que el Dron Blindado defendera
        if (Amigo != null)
        {
            hit = player.position - Amigo.position;
            Debug.DrawLine(player.position, hit);


            transform.position = (hit / hit.magnitude) * 2f;
        } 
       
    }
}
