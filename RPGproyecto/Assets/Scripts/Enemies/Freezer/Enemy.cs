using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // Salud del enemigo
    public string enemyName;
    public int baseAttack;
    public GameObject dieEffect; // Referencia al efecto de muerte (explosión)

    void Start() {}

    void Update() {}

    public delegate void DeathHandler(GameObject enemy);
    public event DeathHandler OnDeath; // Evento que se invocará al morir

    public void TakeDamage(int damage)
    {
        health -= damage; // Resta daño a la salud
        if (health <= 0)
        {
            Die(); // Llama al método Die si la salud es 0 o menos
        }
    }

    void Die()
    {
        OnDeath?.Invoke(gameObject); // Invoca el evento OnDeath
        InstantiateDieEffect(); // Instancia el efecto de muerte
        Destroy(gameObject, 0.1f); // Destruye el enemigo después de un corto periodo
    }

    void InstantiateDieEffect()
    {
        if (dieEffect != null)
        {
            // Instancia el efecto de muerte (explosión) en la posición del enemigo
            GameObject explosion = Instantiate(dieEffect, transform.position, Quaternion.identity);
            // Destruye el efecto de muerte después de 1 segundo (o ajusta según el tamaño del efecto)
            Destroy(explosion, 1f); // 1 segundo es suficiente para que la explosión dure un poco
        }
    }
}
