using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{

    public GameObject kiBallPrefab; // Asigna aquí el prefab de la bola de ki
    public Transform firePoint; // El punto desde donde se disparará la bola de ki
    public float kiBallSpeed = 5f; // Velocidad de la bola de ki

    private Vector2 direction; // Dirección en la que dispara Android 18

    void Update()
    {
        // Determina la dirección basándote en el input
        if (Input.GetButtonDown("attack2")) // Por ejemplo, espacio para disparar
        {
            ShootKiBall();
        }

        // Aquí podrías cambiar la dirección en la que está mirando Android 18
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
        }
    }

    void ShootKiBall()
    {
        // Instancia la bola de ki en la posición del punto de disparo y la rotación actual
        GameObject kiBall = Instantiate(kiBallPrefab, firePoint.position, Quaternion.identity);

        // Asigna la dirección y la velocidad a la bola de ki
        Rigidbody2D rb = kiBall.GetComponent<Rigidbody2D>();
        rb.velocity = direction * kiBallSpeed;
    }
}    
