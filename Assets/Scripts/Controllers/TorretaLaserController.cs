using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserController : TorretaController {

    private void Awake()
    {
        shooting = GetComponentInChildren<TorretaLaserShooting>();
    }

    private void Update()
    {       
        switch (state)
        {
            case EnemyState.Shooting:
                shooting.Cooldown();
                shooting.Shoot();
                break;
        }
    }
}
