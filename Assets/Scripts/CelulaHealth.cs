using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelulaHealth : EnemyHealth {
   
    public override void Die()
    {
        GetComponent<CelulaIAController>().ACellHasDied();   //Avisa al controlador de que ha sido destruida
        Destroy(gameObject);
    }
}
