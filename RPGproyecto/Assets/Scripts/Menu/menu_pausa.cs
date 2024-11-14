using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado = false;


    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (juegoPausado) {

                Reanudar();

            } else {

                Pausa();
            }
        }
    }

    public void Pausa() {

        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }


    public void Reanudar(){

        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

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