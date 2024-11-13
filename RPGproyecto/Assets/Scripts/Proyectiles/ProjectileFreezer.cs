using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFreezer : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform targetPlayer; //Referencia al jugador objetivo
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        rb.velocity = directionToPlayer * speed;
        StartCoroutine(DestroyProjectile());
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
