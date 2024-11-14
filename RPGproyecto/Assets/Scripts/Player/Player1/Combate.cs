using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float damageGolpe;

    // Método Update correctamente nombrado
    private void Update()
    {
    
        // Si el jugador presiona el botón y hay tiempo para el siguiente ataque
        if (Input.GetButtonDown("attack"))
        {
            Golpe(); // Llamamos al método de golpear
        }
    }

    private void Golpe()
    {
        // Detectamos los objetos dentro del área del golpe
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        Debug.Log("Ataque");
        // Iteramos sobre los objetos que se encuentran en la zona de golpe
        foreach (Collider2D colisionador in objetos)
        {
            // Verificamos si el objeto tiene la etiqueta 'Enemigo'
            if (colisionador.CompareTag("enemy"))
            {
                // Si es un enemigo, le aplicamos el daño
                colisionador.transform.GetComponent<Estadisticas>().GetDamage(damageGolpe);
            }
        }
    }

    // Método para dibujar el área de golpe en el editor de Unity
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Cambié 'color.red' a 'Color.red'
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe); // Dibuja un círculo de al rededor de donde el golpe puede impactar
    }
}
