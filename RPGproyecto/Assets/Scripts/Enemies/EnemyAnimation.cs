using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
{
    private Vector2 previousPosition, newPosition;
    private Animator animator;
    private bool isAttacking = false;
    private float attackDuration = 2.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    void Update()
    {
        if (!isAttacking)
        {
            newPosition = transform.position;
            Vector2 velocity = (newPosition - previousPosition) / Time.deltaTime;
            previousPosition = newPosition;

            //Calculate and set speed
            float speed = velocity.magnitude;
            animator.SetFloat("Speed", speed);

            //Set direction parameters
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

    public void TriggerAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
            StartCoroutine(EndAttack());
        }
    }

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }
}
