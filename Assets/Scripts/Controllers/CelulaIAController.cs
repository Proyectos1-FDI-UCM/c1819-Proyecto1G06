using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelulaIAController : MonoBehaviour {

    IAController controller;
    CelulaHealth celHealth;

    private void Awake()
    {
        controller = GetComponentInParent<IAController>();
        celHealth = GetComponent<CelulaHealth>();
    }

    /// <summary>
    /// Avisa a la IA de que ha muerto una célula.
    /// </summary>
    public void ACellHasDied()
    {
        controller.CelulaDestroyed();
    }
}
