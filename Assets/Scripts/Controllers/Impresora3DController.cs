using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impresora3DController : EnemyController {

    public float spawnEvery = 5f;
    public int maxDronAmount = 5;
    public MicrodronHealth microdronPrefab;

    float spawnCooldown;
    int dronAmount = 0;

    private void Update()
    {
        switch (state)
        {
            //Cuando está en el estado Shooting crea microdrones
            case EnemyState.Shooting:   
                SpawnCooldown();        
                SpawnMicrodron();       
                break;
        }
    }

    public override void Sight(RaycastHit2D sight)
    {
        base.Sight(sight);
        if (playerDetected)
        {
            state = EnemyState.Shooting;
        } else
        {
            state = EnemyState.Idle;
        }
    }


    void SpawnMicrodron()
    {
        //Crea el microdron con referencia a la impresora
        if (dronAmount < maxDronAmount && Mathf.Approximately(spawnCooldown, 0f))
        {
            MicrodronHealth dron = Instantiate<MicrodronHealth>(microdronPrefab, transform.position, Quaternion.identity, transform.parent);
            dron.SetImpresora(this);
            dronAmount++;
            spawnCooldown = spawnEvery;
        }
    }

    /// <summary>
    /// Si hay algun dron, reduce su número.
    /// </summary>
    public void DronDied()
    {
        if (dronAmount > 0) dronAmount--;
    }

    /// <summary>
    /// Temporizador de la variable spawnCooldown
    /// </summary>
    void SpawnCooldown()
    {
        if (spawnCooldown > 0f)
        {
            spawnCooldown -= Time.deltaTime;
        }
        else spawnCooldown = 0;
    }
}
