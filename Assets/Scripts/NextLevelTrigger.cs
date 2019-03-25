using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour {

    public string sceneName;

    /// <summary>
    /// Recarga la escena al entrar el jugador
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if(health != null)
        {
            GameManager.instance.LoadScene(sceneName);
        }
    }
}
