using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingLaser : MonoBehaviour {

    public Transform shootingPoint;
    public Transform followPoint;

    LineRenderer lr;
    RaycastHit2D hit;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void OnEnable()
    {   
        lr.enabled = true;
        lr.startWidth = 0.15f;
        lr.endWidth = 0.15f;
        lr.SetPosition(0, shootingPoint.position);
        lr.SetPosition(1, followPoint.position);
    }

    void Update()
    {
        lr.SetPosition(1, followPoint.position);
        hit = Physics2D.Linecast(shootingPoint.position, followPoint.position, LayerMask.GetMask("Player"));
        if(hit.transform.GetComponent<PlayerHealth>() != null)
        {
            hit.transform.GetComponent<PlayerHealth>().TakeDamage();
        }
    }

    private void OnDisable()
    {
        followPoint.position = shootingPoint.position;
        lr.enabled = false;
    }

}
