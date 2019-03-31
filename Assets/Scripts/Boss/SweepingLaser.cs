using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingLaser : MonoBehaviour, IBossAttack {

    LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Attack()
    {
        
    }

}
