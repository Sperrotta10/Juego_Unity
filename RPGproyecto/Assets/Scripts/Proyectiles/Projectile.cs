using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    private Vector3 moveDirection = Vector3.right; // Dirección por defecto

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Establece la velocidad del Rigidbody2D según la dirección
        rb.velocity = moveDirection * moveSpeed;
    }

    // Método para establecer la dirección del proyectil y ajustar la rotación
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
        rb.velocity = moveDirection * moveSpeed;

        // Ajustar la rotación del proyectil según la dirección
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        // Ya no es necesario mover el proyectil aquí
    }
}
