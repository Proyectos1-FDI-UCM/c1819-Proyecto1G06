using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class BombonadeO2 : MonoBehaviour {

    public int amount = 1;

    Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    /// <summary>
    /// Suma una cantidad amount de salud al jugador y se destruye.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (interactable.interactable)
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(amount);
                Destroy(gameObject);
            }
        }
    }

}
