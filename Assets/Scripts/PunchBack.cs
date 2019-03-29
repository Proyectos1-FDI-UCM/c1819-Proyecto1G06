using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBack : MonoBehaviour {

    Transform player;

    float armLength;
    public float punchKnockback;

	void Start ()
    {
        player = GameManager.instance.player.transform;
        armLength = GetComponent<VoluntarioController>().meleeDistance;
	}
	
	/// <summary>
    /// Si el jugador se encuentra al alcanze del enemigo, recibe un empuje
    /// en dirección opuesta al enemigo y de punchKnockback potencia.
    /// </summary>
    public void Punch()
    {
        if (Vector3.Distance(player.position, transform.position) <= armLength)
        {
            player.GetComponent<Knockback>().TakeKnockback(player.position - transform.position, punchKnockback);
            player.GetComponent<PlayerHealth>().TakeDamage();
        }
    }
}
