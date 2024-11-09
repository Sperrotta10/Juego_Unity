using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesPerWave = 10;
    public float timeBetweenWaves = 3f;
    private float countdown = 2f;
    public Transform spawnLocation;
    private int currentWave = 0; //Oleada actual
    private List<GameObject> spawnedEnemies = new List<GameObject>(); //Lista de enemigos instanciados

    void Update()
    {
        // Verifica si hay enemigos vivos
        if (spawnedEnemies.Count == 0 && currentWave < 5) //Solo genera una nueva oleada si no hay enemigos y no se ha alcanzado el límite de oleadas
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        currentWave++; // Incrementa la oleada actual
        Debug.Log("Iniciando Oleada " + currentWave);

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

        
        while (spawnedEnemies.Count > 0)
        {
            yield return null;
        }

        
        if (currentWave >= 5)
        {
            Debug.Log("¡Todas las oleadas completadas!");
            
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)spawnLocation.position + Random.insideUnitCircle * 2f; 
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); 
        spawnedEnemies.Add(enemy);
        
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.OnDeath += HandleEnemyDeath;
        }
    }

    void HandleEnemyDeath(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }
}