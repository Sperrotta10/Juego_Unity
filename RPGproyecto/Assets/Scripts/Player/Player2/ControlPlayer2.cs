using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer2 : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public GameObject kiBallPrefab;
    public float kiBallSpeed = 5f; //Velocidad de la bola de ki

    public enum PlayerState
    {
        walk,
        attack2
    }

    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    private PlayerState currentState = PlayerState.walk;

    //Direcciones de disparo
    private Vector2 shootDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal2");
            input.y = Input.GetAxisRaw("Vertical2");

            // Si se presiona el botón de ataque
            if (Input.GetButtonDown("attack2") && currentState != PlayerState.attack2)
            {
                StartCoroutine(AttackCo());
            }

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                shootDirection = input.normalized;

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        //Para la transición de caminata
        animator.SetBool("isMoving", isMoving);
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack2;

         //Espera 0.1 segundos para que se complete la animación de apuntado
        yield return new WaitForSeconds(0.2f);

        //Instancia la bola de ki y establece su dirección solo una vez
        ShootKiBall();

        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    private void ShootKiBall()
    {
        Projectile kiBall = Instantiate(kiBallPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();

        Vector3 direction = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"), 0);
        kiBall.SetDirection(direction);
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.3f, solidObjectsLayer) == null;
    }
}
