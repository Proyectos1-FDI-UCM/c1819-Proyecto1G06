using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelulaHealth : EnemyHealth {

    public override void Die()
    {
        //Decirle a la IA que ha muerto
        Destroy(gameObject);
    }
}
