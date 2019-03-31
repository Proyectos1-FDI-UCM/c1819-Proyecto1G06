using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelulaHealth : EnemyHealth {

    CelulaIAController controller { get { return GetComponent<CelulaIAController>(); } }

    public override void TakeDamage(float amount)
    {
        if (controller.Vulnerable)
            base.TakeDamage(amount);
    }

    public override void Die()
    {
        controller.ACellHasDied();   //Avisa al controlador de que ha sido destruida
        Destroy(gameObject);
    }
}
