using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_buttons : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Obtén el componente AudioSource del botón
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (audioSource != null && !audioSource.enabled)
        {
            audioSource.enabled = true;
        }
        audioSource.Play();
    }
}
