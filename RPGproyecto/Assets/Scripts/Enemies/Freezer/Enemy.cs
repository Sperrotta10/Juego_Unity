using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // Salud del enemigo

    // Delegado para el evento OnDeath
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
        Destroy(gameObject); // Destruye el objeto del enemigo
    }
}
