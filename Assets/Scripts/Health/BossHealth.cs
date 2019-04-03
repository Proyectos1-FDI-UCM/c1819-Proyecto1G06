using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        GameManager.instance.ui.UpdateBossHealth(curHealth, maxHealth);
    }
}
