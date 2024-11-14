using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_game : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject gameOverCanvas; // El Canvas de Game Over
    public AudioClip gameOverClip; // El clip de audio de Game Over
    private AudioSource audioSource; // El componente AudioSource

    private bool player1Dead = false;
    private bool player2Dead = false;

    private void Start()
    {
        // Asegurarnos de que el canvas de Game Over esté desactivado al inicio
        gameOverCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
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
            
            // Reproducir el sonido cuando ambos jugadores mueren
            if (gameOverClip != null) {
                audioSource.PlayOneShot(gameOverClip);
            } else {
                Debug.LogWarning("No se ha asignado un AudioClip de Game Over.");
            }
        
            gameOverCanvas.SetActive(true);
        }
    }

}
