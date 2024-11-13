using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    private bool isFacingRight = true;

    // Update is called once per frame
    void Update()
    {

        Transform closestPlayer = GetClosestPlayer();
        
        if (closestPlayer != null)
        {
            //Mueve al enemigo hacia la posición del jugador más cercano
            if (Vector2.Distance(transform.position, closestPlayer.position) > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, closestPlayer.position, speed * Time.deltaTime);
            }
            else
            {
                Attack();
            }
            
            bool isPlayerRight = transform.position.x < closestPlayer.position.x;
            //Flip(isPlayerRight);
        }
    }

    private void Attack()
    {
        //Debug.Log("Atacar");
    }

    private Transform GetClosestPlayer()
    {
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        if (distanceToPlayer1 < distanceToPlayer2)
        {
            return player1;
        }
        else
        {
            return player2;
        }
    }

    /*private void Flip(bool isPlayerRight)
    {
        if ((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }*/
}