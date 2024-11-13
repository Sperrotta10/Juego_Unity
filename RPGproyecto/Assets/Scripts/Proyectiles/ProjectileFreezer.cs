using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFreezer : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform targetPlayer; //Referencia al jugador objetivo
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (targetPlayer != null)
        {
            LaunchProjectile();
        }
        else
        {
            Debug.LogError("El objetivo del proyectil no está asignado.");
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        targetPlayer = target;
    }

    private void LaunchProjectile()
    {
        Vector2 directionToPlayer = (targetPlayer.position - transform.position).normalized;
        SetDirection(directionToPlayer);
        StartCoroutine(DestroyProjectile());
    }

    // Método para establecer la dirección del proyectil y ajustar la rotación
    private void SetDirection(Vector2 direction)
    {
        // Normaliza y ajusta la dirección
        Vector2 moveDirection = direction.normalized;
        rb.velocity = moveDirection * speed;

        // Ajusta la rotación del proyectil según la dirección
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private IEnumerator DestroyProjectile()
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
