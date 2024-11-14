using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerController : MonoBehaviour
{
    public WaveSpawner spawner1; // Primer Spawner
    public WaveSpawner spawner2; // Segundo Spawner

    public GameObject winCanvas; // Canvas de victoria
    [SerializeField] private AudioClip musicaVictoria; // Música de victoria
    private AudioSource audioSource; // El componente AudioSource para controlar la música

    // Referencia al AudioSource de la música general del juego
    public GameObject controladorDeJuego; // Referencia al GameObject que controla la música general
    private AudioSource audioSourceJuego; // AudioSource del controlador de música

    // Volúmenes para las diferentes músicas
    [SerializeField, Range(0f, 1f)] private float volumenVictoria = 0.5f; // Volumen para la música de victoria

    private bool hasWon = false; // Bandera para asegurarse de que solo se llame a la victoria una vez

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (winCanvas != null)
        {
            winCanvas.SetActive(false); // Ocultar la pantalla de victoria inicialmente
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

        // Iniciar los spawners
        spawner1.StartSpawning();
        spawner2.StartSpawning();
    }

    void Update()
    {
        // Verificar si ambos spawners han terminado de generar todas las oleadas y enemigos
        if (!hasWon && spawner1.IsFinished() && spawner2.IsFinished())
        {
            // Solo ejecutar esto si no se ha ejecutado antes
            ShowWinScreen(); // Si ambos spawners han terminado, muestra la pantalla de victoria
            hasWon = true; // Marcar que ya se ha mostrado la victoria
        }
    }

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

    // Método para reproducir la música de victoria
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
}
