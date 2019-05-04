using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScreenController : MonoBehaviour {

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null) anim.SetBool("ShowControls", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null) anim.SetBool("ShowControls", false);
    }
}
