using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarAnimation : MonoBehaviour {

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void DisableAnimator()
    {
        animator.enabled = false;
    }

}
