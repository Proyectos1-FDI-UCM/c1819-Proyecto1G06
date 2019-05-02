using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingLaser : MonoBehaviour, IBossAttack1 {

    public Transform shootingPoint;
    public Transform followPoint;

    protected LineRenderer lr;

    public float attackTime = 10;
    public float AttackTime
    {
        get
        {
            return attackTime;
        }
    }

    public AudioClip laserClip;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void OnEnable()
    {
        followPoint.position = shootingPoint.position;
        lr.enabled = true;
        lr.widthMultiplier = 1f;
        lr.SetPosition(0, shootingPoint.position);
        lr.SetPosition(1, followPoint.position);
    }

    public virtual void Update()
    {
        RaycastHit2D hit;
        lr.SetPosition(1, followPoint.position);
        hit = Physics2D.Linecast(shootingPoint.position, followPoint.position, LayerMask.GetMask("Player"));
        if(hit.transform == GameManager.instance.player.transform)
        {
            GameManager.instance.onPlayerTookDamage();
        }
    }

    private void OnDisable()
    {
        lr.enabled = false;
    }

    public void ToggleAttack(bool active)
    {
        this.enabled = active;       
    }
}
