using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        StartCoroutine(PlaySoundAndChangeScene());
    }

    private IEnumerator PlaySoundAndChangeScene()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length); // Espera el tiempo de duración del sonido
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            Debug.Log("Salir del juego...");
            StartCoroutine(QuitAfterSound());
        }
    }

    private IEnumerator QuitAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length); // Espera a que el sonido termine
        Application.Quit();
    }
}
