using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour {

    private EnemyController controller;
    Transform playerTransform;
    RaycastHit2D[] sight = new RaycastHit2D[1];
    Collider2D col;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        playerTransform = GameManager.instance.player.transform;
    }

    /// <summary>
    /// Si ve al jugador, actualiza playerDetected del controlador
    /// </summary>
    void Update ()
    {
        col.Raycast(playerTransform.position - transform.position, sight, 100, LayerMask.GetMask("Player", "Environment")); //Los resultados están ordenados, solo devuelve el primer objeto
        controller.Sight(sight[0]);
    }
}
