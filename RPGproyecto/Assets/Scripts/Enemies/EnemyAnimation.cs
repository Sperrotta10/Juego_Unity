using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Vector2 previousPosition, newPosition;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    void Update()
    {
        newPosition = transform.position;
        Vector2 velocity = (newPosition - previousPosition) / Time.deltaTime;
        previousPosition = newPosition;

        // Calcula la magnitud de la velocidad
        float speed = velocity.magnitude;

        // Establece el parámetro "Speed"
        animator.SetFloat("Speed", speed);

        // Controla las direcciones de movimiento
        if (speed > 0.1f)
        {
            animator.SetFloat("moveX", velocity.x);
            animator.SetFloat("moveY", velocity.y);
        }
        else
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
        }
    }
}