using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour, IBossAttack1 {

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public int numSpawns = 3;

    int spawnPointsSize = 0;

    public float attackTime = 2;
    public float AttackTime { get { return attackTime; } }

    private void OnEnable()
    {
        spawnPointsSize = spawnPoints.Length;
        Spawn();
    }

    /// <summary>
    /// Spawnea enemigos en una posición aleatoria
    /// </summary>
    void Spawn()
    {
        for(int i = 0; i < numSpawns; i++)
        {
            Transform usedSpawnPoint = spawnPoints[Random.Range(0, spawnPointsSize)];
            Transform.Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], usedSpawnPoint.position, Quaternion.identity, transform);
            PlaceLast(spawnPoints, usedSpawnPoint);
            spawnPointsSize--;
        }
    }

    /// <summary>
    /// Coloca item en la última posición y el resto de componentes los deja ordenados
    /// </summary>
    void PlaceLast(Transform[] array, Transform item)
    {
        int pos = Search(array, item);
        for(; pos < array.Length - 1; pos++)
        {
            array[pos] = array[pos + 1];
        }

        array[pos] = item;
    }

    /// <summary>
    /// Busca item en el array
    /// </summary>
    /// <returns>-1 si no lo encuentra y la posición si lo encuentra</returns>
    int Search(Transform[] array, Transform item)
    {
        int i = 0;
        while (i < array.Length && array[i] != item) i++;
        return i == array.Length ? -1 : i;
    }

    public void ToggleAttack(bool active)
    {
        this.enabled = active;
    }
}
