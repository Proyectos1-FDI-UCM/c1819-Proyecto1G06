using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrodronHealth : EnemyHealth {

    Impresora3DController impresora;

    /// <summary>
    /// Asigna una impresora al objeto
    /// </summary>
    /// <param name="impresora">La impresora creadora</param>
    public void SetImpresora(Impresora3DController impresora)
    {
        this.impresora = impresora;
    }

    /// <summary>
    /// Muere y avisa a la impresora de ello
    /// </summary>
    public override void Die()
    {
        impresora.DronDied();
        base.Die();
    }
}
