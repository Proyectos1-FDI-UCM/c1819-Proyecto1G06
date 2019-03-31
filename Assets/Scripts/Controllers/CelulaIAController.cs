using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelulaIAController : MonoBehaviour {

    IAController controller;
    CelulaHealth celHealth;

    bool vulnerable = false;
    public bool Vulnerable { get { return vulnerable; } set { vulnerable = value; } }

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
