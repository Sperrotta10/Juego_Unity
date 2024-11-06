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

    private void Start(){

        health = maxHealth;
        healthbar.UpdateHealthbar(maxHealth,health);
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // esto es un evento de click para el box colider, es provisional para ver como funciona el damage y barra de vida de los jugadores
    private void OnMouseDown(){
        StartCoroutine(GetDamage());
    }

    IEnumerator GetDamage(){

        float damageDuration = 0.1f;
        float damage = Random.Range(1f,5f);
        health -= damage;
        healthbar.UpdateHealthbar(maxHealth,health);

        if(health > 0) {

            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(damageDuration);
            spriteRenderer.color = Color.white;

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
}
