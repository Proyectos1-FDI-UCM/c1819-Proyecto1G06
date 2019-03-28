using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoluntarioHealth : EnemyHealth {

    VoluntarioController controller;

    private void Start()
    {
        controller = GetComponent<VoluntarioController>();       
    }

    /// <summary>
    /// Si es vulnerable, recibe daño y actualiza su barra de vida
    /// </summary>
    /// <param name="amount"></param>
    public override void TakeDamage(float amount)
    {
        switch (controller.GetState())
        {
            case (EnemyState.Idle):
            case (EnemyState.Chasing):
            case (EnemyState.Fleeing):
                //controller.
                break;
            case (EnemyState.Shooting):
                controller.Stun();
                break;
            case (EnemyState.Stunned):
                base.TakeDamage(amount);
                GameManager.instance.ui.UpdateBossHealth(curHealth, maxHealth);
                break;
        }          
    }

    
}
