using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;

    /*
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("gun"))
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy();
            }
        }
    }
    */

    public void SpawnEnemy()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Lógica de spawn del enemigo
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 5f; // Cambia 5f por la distancia deseada alrededor del objeto controlador
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }

    }
}