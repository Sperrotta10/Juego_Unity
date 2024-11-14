using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuGameOver : MonoBehaviour
{
    public void Reiniciar() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Función para regresar al menú principal
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Asegúrate de que el juego esté corriendo
        SceneManager.LoadScene("Menu_game"); // Cambia "MainMenu" por el nombre de tu escena principal
    }
}
