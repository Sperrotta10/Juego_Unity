using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public int enemiesPerWave = 10; // Número de enemigos por oleada
    public float timeBetweenWaves = 3f; // Tiempo entre oleadas
    private float countdown; // Temporizador entre oleadas
    public Transform spawnLocation; // Ubicación de aparición
    private int currentWave = 0; // Oleada actual
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // Lista de enemigos generados

    void Start()
    {
        countdown = timeBetweenWaves; // Inicia el contador con el tiempo entre oleadas
    }

    void Update()
    {

        // Eliminar enemigos muertos de la lista
        RemoveDeadEnemies();
        
        // Si no hay enemigos vivos y aún no hemos alcanzado el máximo de oleadas
        Debug.Log(spawnedEnemies.Count);
        if (spawnedEnemies.Count == 0 && currentWave < 5) 
        {
            countdown -= Time.deltaTime; // Resta tiempo al contador
            if (countdown <= 0f) // Si el contador llegó a 0, se genera una nueva oleada
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves; // Reinicia el contador para la próxima oleada
            }
        }
    }

    IEnumerator SpawnWave()
    {
        currentWave++; // Incrementa la oleada actual
        Debug.Log("Iniciando Oleada " + currentWave);

        // Genera los enemigos de esta oleada
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f); // Espera 1 segundo entre la creación de enemigos
        }

        // Espera hasta que todos los enemigos hayan muerto antes de proceder
        while (spawnedEnemies.Count > 0)
        {
            yield return null;
        }

        // Cuando todos los enemigos de la oleada actual han muerto
        if (currentWave >= 5)
        {
            Debug.Log("¡Todas las oleadas completadas!");
        }
    }

    void SpawnEnemy()
    {
        // Define la posición de aparición aleatoria dentro de un rango de 2 unidades desde el punto de spawn
        Vector2 spawnPosition = (Vector2)spawnLocation.position + Random.insideUnitCircle * 2f; 
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Crea el enemigo
        spawnedEnemies.Add(enemy); // Lo agrega a la lista de enemigos generados

    }

    void RemoveDeadEnemies()
    {
        // Recorre la lista de enemigos y elimina a los que ya han sido destruidos
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null) // Si el GameObject del enemigo ha sido destruido
            {
                spawnedEnemies.RemoveAt(i); // Elimina al enemigo de la lista
            }
        }
    }
}
