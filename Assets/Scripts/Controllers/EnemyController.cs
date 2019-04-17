using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    protected GameObject player { get { return GameManager.instance.player; } }
    protected EnemyState state = EnemyState.Idle;
    protected bool playerDetected = false;

    public bool GetPlayerDetected()
    {
        return playerDetected;
    }

    /// <summary>
    /// Actualiza si está detectando al jugador
    /// </summary>
    public virtual void Sight(RaycastHit2D sight)
    {
        playerDetected = sight.transform == GameManager.instance.player.transform;
    }
}
