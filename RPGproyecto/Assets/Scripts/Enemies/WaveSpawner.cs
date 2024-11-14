using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public int enemiesPerWave = 10; // Número de enemigos por oleada
    public float timeBetweenWaves = 3f; // Tiempo entre oleadas
    public int oleadas = 2; // numero de oleadas
    private float countdown; // Temporizador entre oleadas
    public Transform spawnLocation; // Ubicación de aparición
    private int currentWave = 0; // Oleada actual
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // Lista de enemigos generados
    private bool isFinished = false; // Indica si todas las oleadas han terminado

    /*
    public GameObject winCanvas; // Referencia al Canvas de victoria
    [SerializeField] private AudioClip musicaVictoria; // Música de derrota
    private AudioSource audioSource; // El componente AudioSource

    // Referencia al AudioSource de la música general del juego
    public GameObject controladorDeJuego; // Referencia al GameObject que controla la música general
    private AudioSource audioSourceJuego; // AudioSource del controlador de música

    // Volúmenes para las diferentes músicas
    [SerializeField, Range(0f, 1f)] private float volumenVictoria = 0.5f; // Volumen para la música de victoria
    */

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();

        countdown = timeBetweenWaves; // Inicia el contador con el tiempo entre oleadas

        /*
        if (winCanvas != null)
        {
            winCanvas.SetActive(false); // Asegura que el Canvas de victoria esté oculto al principio
        }

        // Obtener el AudioSource del controlador del juego
        if (controladorDeJuego != null)
        {
            audioSourceJuego = controladorDeJuego.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("No se encontró el GameObject del controlador de música.");
        }

        // Asegurarse de que haya un AudioSource en el GameObject
        if (audioSourceJuego == null)
        {
            Debug.LogError("No se encuentra un AudioSource en el controlador del juego.");
            return;
        }

        // Reproducir la música de fondo del juego si no está sonando
        if (!audioSourceJuego.isPlaying)
        {
            audioSourceJuego.Play();
        }
        */

    }

    void Update()
    {

        // Eliminar enemigos muertos de la lista
        RemoveDeadEnemies();

        // Si no hay enemigos vivos y aún no hemos alcanzado el máximo de oleadas
        if (spawnedEnemies.Count == 0 && currentWave < oleadas) 
        {
            countdown -= Time.deltaTime; // Resta tiempo al contador
            if (countdown <= 0f) // Si el contador llegó a 0, se genera una nueva oleada
            {
                StartSpawning();
                countdown = timeBetweenWaves; // Reinicia el contador para la próxima oleada
            }
        }

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnWave());
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
        if (currentWave >= oleadas && spawnedEnemies.Count == 0)
        {
            isFinished = true;
            Debug.Log("¡Todas las oleadas completadas!");
            //ShowWinScreen();
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


    /*
    // Función para mostrar la pantalla de victoria
    void ShowWinScreen()
    {

        // Detener la música del juego y reproducir la música de victoria
        if (audioSourceJuego != null && audioSourceJuego.isPlaying)
        {
            audioSourceJuego.Pause(); // Pausa la música general del juego
        }

        ReproducirMusicaVictoria();

        if (winCanvas != null)
        {
            winCanvas.SetActive(true); // Muestra el Canvas de victoria
        }
    }

    // Método para reproducir la música de derrota
    private void ReproducirMusicaVictoria()
    {
        Debug.Log("VICTORIA");
        if (musicaVictoria != null)
        {
            audioSource.clip = musicaVictoria;
            audioSource.loop = false;
            audioSource.volume = volumenVictoria; // Establecer el volumen de la música de victoria
            audioSource.Play();
        }
    }
    */

    // Método para saber si todas las oleadas han terminado
    public bool IsFinished()
    {
        return isFinished;
    }
}
