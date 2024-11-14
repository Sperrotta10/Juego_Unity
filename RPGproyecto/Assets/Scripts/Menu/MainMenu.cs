using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Método que se llamará cuando se presione el botón "Easy"
    public void OnEasyButtonPressed()
    {
        // Cargar la escena del nivel fácil
        SceneManager.LoadScene("nivel_easy");
    }

    // Método que se llamará cuando se presione el botón "Medium"
    public void OnMediumButtonPressed()
    {
        // Cargar la escena del nivel medio
        SceneManager.LoadScene("nivel_medium");
    }

    // Método que se llamará cuando se presione el botón "Hard"
    public void OnHardButtonPressed()
    {
        // Cargar la escena del nivel difícil
        SceneManager.LoadScene("nivel_hard");
    }

    // Método que se llamará cuando se presione el botón "salir"
    public void QuitGame()
    {
        Debug.Log("Salir del juego...");
        Application.Quit();
    }
}
