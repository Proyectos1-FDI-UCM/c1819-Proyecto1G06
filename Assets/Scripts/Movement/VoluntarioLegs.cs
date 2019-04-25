using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioLegs : MonoBehaviour {

    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    public void GetDamaged()
    {
        anim.SetLayerWeight(1, 1);
        Invoke("GetUnDamaged", GetComponentInParent<VoluntarioHealth>().damagedTime);
    }

    public void GetUnDamaged()
    {
        anim.SetLayerWeight(1, 0);
    }
}
