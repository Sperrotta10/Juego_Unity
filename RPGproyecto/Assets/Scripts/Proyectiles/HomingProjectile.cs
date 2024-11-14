using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private Transform target; // Jugador al que seguirá el proyectil
    private float speed = 5f; // Velocidad del proyectil
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No se encontró el componente Rigidbody2D en el proyectil.");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            Destroy(gameObject); // Destruye el proyectil si el objetivo no existe
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target == null)
        {
            Debug.LogError("Se intentó asignar un objetivo nulo al proyectil.");
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Aquí puedes agregar lógica adicional para cuando el proyectil colisiona
        Destroy(gameObject);
    }
}