using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2ScreenController : MonoBehaviour {

    public Vector2 intervalAttacks;

    enum ScreenState { Idle, Shooting }

    IBossAttack1[] attacks;
    ScreenState state;
    Animator anim;
    float timer;

    private void Awake()
    {       
        state = ScreenState.Idle;
        timer = Random.Range(intervalAttacks.x, intervalAttacks.y);
        anim = GetComponent<Animator>();

        attacks = new IBossAttack1[2];
        attacks[0] = GetComponent<SweepingLaser2>();
        attacks[1] = GetComponent<BulletStorm>();
    }

    private void Update()
    {
        if(state == ScreenState.Idle)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) timer = 0;           
        }
        if(timer <= 0)
        {
            state = ScreenState.Shooting;
            anim.SetTrigger("Shoot");

            timer = Random.Range(intervalAttacks.x, intervalAttacks.y);

            int index = Random.Range(0, attacks.Length);
            for (int i = 0; i < attacks.Length; i++)
            {
                attacks[i].ToggleAttack(false);
            }
            attacks[index].ToggleAttack(true);
        }
    }

    public void GoIdle()
    {
        state = ScreenState.Idle;
        for (int i = 0; i < attacks.Length; i++)
        {
            attacks[i].ToggleAttack(false);
        }
    }
}
