using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta del objeto que debe tocar el controlador
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        // Lógica de spawn del enemigo
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 5f; // Cambia 5f por la distancia deseada alrededor del objeto controlador
        Quaternion spawnRotation = Quaternion.identity; // Opcional: puedes especificar una rotación inicial si es necesario
        Instantiate(enemyPrefab, spawnPosition, spawnRotation);
    }
}