using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFreezer : MonoBehaviour
{
    [SerializeField] private float speed; // Velocidad del proyectil
    [SerializeField] private Transform player1; // Referencia al primer jugador
    [SerializeField] private Transform player2; // Referencia al segundo jugador
    private Rigidbody2D rb; // Componente Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene el componente Rigidbody2D

        if (player1 == null || player2 == null)
        {
            Debug.LogError("Los jugadores no están asignados en el Inspector.");
            return; // Salir si los jugadores no están asignados
        }

        Transform closestPlayer = GetClosestPlayer(); // Encuentra al jugador más cercano
        if (closestPlayer != null)
        {
            LaunchProjectile(closestPlayer); // Lanza el proyectil hacia el jugador más cercano
        }
    }

    private Transform GetClosestPlayer()
    {
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        // Retorna el transform del jugador más cercano
        return distanceToPlayer1 < distanceToPlayer2 ? player1 : player2;
    }

    private void LaunchProjectile(Transform targetPlayer)
    {
        Vector2 directionToPlayer = (targetPlayer.position - transform.position).normalized; // Calcula la dirección hacia el jugador
        rb.velocity = directionToPlayer * speed; // Establece la velocidad del proyectil
        StartCoroutine(DestroyProjectile());
    }

    IEnumerator DestroyProjectile()
    {
        float destroyTime = 5f;
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}