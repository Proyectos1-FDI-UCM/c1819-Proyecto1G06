using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelulaHealth : IAScreenHealth {

    IAController ia;

    public override void Awake()
    {
        ia = GetComponentInParent<IAController>();
        base.Awake();
    }

    public override void Die()
    {
        ia.CelulaDestroyed();  //Avisa al controlador de que ha sido destruida
        Destroy(gameObject);
    }
}
