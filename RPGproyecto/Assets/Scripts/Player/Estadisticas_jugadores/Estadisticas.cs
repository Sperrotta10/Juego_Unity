using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estadisticas : MonoBehaviour
{
    [SerializeField] private Health_bar healthbar;
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private float maxHealth;
    private float health;
    private SpriteRenderer spriteRenderer;

    // Variables de control de daño
    private float tiempoDeDaño = 0f;
    private float tiempoMaximoDeDaño = 0.1f; // Duración del efecto de cambio de color

    private void Start(){

        health = maxHealth;
        healthbar.UpdateHealthbar(maxHealth,health);
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // esto es un evento de click para el box colider, es provisional para ver como funciona el damage y barra de vida de los jugadores
    /*
    private void OnMouseDown(){
        StartCoroutine(GetDamage());
    }
    */

    public void GetDamage(float damage){

        health -= damage;
        healthbar.UpdateHealthbar(maxHealth,health);

        if(health > 0) {

            spriteRenderer.color = Color.red;
            tiempoDeDaño = tiempoMaximoDeDaño;

        } else {
            // Cambia el estado a muerto
            Animator animator = GetComponent<Animator>();
            animator.SetBool("isDead", true);
            // Instanciar el efecto de muerte
            GameObject explosion = Instantiate(dieEffect, transform.position, Quaternion.identity);
            // Reproducir sonido de explosión
            AudioSource audioSource = explosion.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject,0.1f);
        }

    }

    private void Update()
    {
        // Si el tiempo de daño es mayor que 0, descontamos el tiempo
        if (tiempoDeDaño > 0)
        {
            tiempoDeDaño -= Time.deltaTime; // Resta el tiempo transcurrido

            // Si el tiempo se ha agotado, restauramos el color
            if (tiempoDeDaño <= 0)
            {
                spriteRenderer.color = Color.white; // Restaurar color original
            }
        }
    }
}
