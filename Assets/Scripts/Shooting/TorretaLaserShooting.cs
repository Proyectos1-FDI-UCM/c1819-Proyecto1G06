using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserShooting : MkIShooting {

    bool damaging = false;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if (!shooting)
            base.Update();
        if (damaging)
        {
            RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position, transform.right, Mathf.Infinity, LayerMask.GetMask("Environment", "Player"));
            if (hit.transform == player)
            {
                GameManager.instance.onPlayerTookDamage();
            }
        }
    }

    public void StartDamaging()
    {
        damaging = true;
    }

    public void StopDamaging()
    {
        damaging = false;
    }

    public void StopShooting()
    {
        shooting = false;
        ResetCooldown();
    }
}
