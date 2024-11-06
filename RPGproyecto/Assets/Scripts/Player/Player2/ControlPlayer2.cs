using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer2 : MonoBehaviour
{
    public float moveSpeed;

    public LayerMask solidObjectsLayer;

    public enum PlayerState
    {
        walk,
        attack2
    }

    private bool isMoving;

    private Vector2 input;

    private Animator animator;

    private PlayerState currentState = PlayerState.walk; // currentState

    //For the animations
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal2");
            input.y = Input.GetAxisRaw("Vertical2");

            if (Input.GetButtonDown("attack2") && currentState != PlayerState.attack2) // Cambiar input.GetButtonDown por Input.GetButtonDown
            {
                StartCoroutine(AttackCo());
            }

            //If you can remove the diagonal movement
            //if(input.x !=0) input.y = 0;

            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX",input.x);
                animator.SetFloat("moveY",input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        //For the walk transitions
        animator.SetBool("isMoving", isMoving);
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack2;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false; 
    }

    //Adding colisions
    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos,0.3f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }
}
