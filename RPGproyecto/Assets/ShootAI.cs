using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShoots = 5f;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    private void Start()
    {
        // Asigna los jugadores si no se han asignado en el Inspector
        if (player1 == null)
            player1 = GameObject.Find("Player 1")?.transform;

        if (player2 == null)
            player2 = GameObject.Find("Player 2")?.transform;

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {   
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShoots);

            Transform closestPlayer = GetClosestPlayer();
            
            // Dispara solo si el jugador más cercano está dentro del rango
            if (closestPlayer != null && Vector2.Distance(transform.position, closestPlayer.position) <= attackRange)
            {
                ShootProjectile(closestPlayer);
            }
        }
    }

    private Transform GetClosestPlayer()
    {
        // Verifica que los jugadores aún existen
        if (player1 == null && player2 == null) return null;

        if (player1 != null && player2 != null)
        {
            float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
            float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);
            return distanceToPlayer1 < distanceToPlayer2 ? player1 : player2;
        }
        else if (player1 != null)
        {
            return player1;
        }
        else if (player2 != null)
        {
            return player2;
        }

        return null;
    }

    private void ShootProjectile(Transform target)
    {
        // Instancia el proyectil y asigna el objetivo
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ProjectileFreezer projectileScript = projectileInstance.GetComponent<ProjectileFreezer>();

        if (projectileScript != null)
        {
            projectileScript.SetTarget(target);
        }
    }
}
