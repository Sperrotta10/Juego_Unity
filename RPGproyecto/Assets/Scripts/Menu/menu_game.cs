using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_game : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject gameOverCanvas; // El Canvas de Game Over

    [SerializeField] private AudioClip musicaJuego;  // Música de fondo del juego
    [SerializeField] private AudioClip musicaDerrota; // Música de derrota
    private AudioSource audioSource; // El componente AudioSource

    // Volúmenes para las diferentes músicas
    [SerializeField, Range(0f, 1f)] private float volumenJuego = 0.05f;  // Volumen para la música del juego
    [SerializeField, Range(0f, 1f)] private float volumenDerrota = 0.5f; // Volumen para la música de derrota

    private bool player1Dead = false;
    private bool player2Dead = false;

    private void Start()
    {
        // Asegurarnos de que el canvas de Game Over esté desactivado al inicio
        gameOverCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        // Asegurarse de que haya un AudioSource en el GameObject
        if (audioSource == null)
        {
            Debug.LogError("No se encuentra un AudioSource en el GameObject.");
            return;
        }

        // Comenzar con la música de fondo del juego
        ReproducirMusicaJuego();
    }

    private void Update()
    {
        // Comprobamos si los jugadores están muertos
        if (player1 == null && !player1Dead)
        {
            player1Dead = true;
            CheckGameOver();
        }

        if (player2 == null && !player2Dead)
        {
            player2Dead = true;
            CheckGameOver();
        }
    }

    private void CheckGameOver()
    {
        // Si ambos jugadores están muertos, mostramos el Canvas de Game Over
        if (player1Dead && player2Dead)
        {
            
            // Detener la música actual y reproducir la música de derrota
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Detener la música actual
            }

            ReproducirMusicaDerrota();
        
            gameOverCanvas.SetActive(true);
        }
    }

    // Método para reproducir la música de juego
    private void ReproducirMusicaJuego()
    {
        if (musicaJuego != null && !audioSource.isPlaying)
        {
            audioSource.clip = musicaJuego;
            audioSource.loop = true;
            audioSource.volume = volumenJuego; // Establecer el volumen de la música del juego
            audioSource.Play();
        }
    }


    // Método para reproducir la música de derrota
    private void ReproducirMusicaDerrota()
    {
        if (musicaDerrota != null)
        {
            audioSource.clip = musicaDerrota;
            audioSource.loop = false;
            audioSource.volume = volumenDerrota; // Establecer el volumen de la música de derrota
            audioSource.Play();
        }
    }
}
