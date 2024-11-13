using UnityEngine;

public class FollowAI : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    private Transform targetPlayer;

    void Update()
    {
        targetPlayer = GetClosestPlayer();
        if (targetPlayer != null)
        {
            //Lógica para mover el enemigo hacia el jugador objetivo
            Vector2 direction = (targetPlayer.position - transform.position).normalized;
            transform.position += (Vector3)direction * Time.deltaTime;
        }
    }

    private Transform GetClosestPlayer()
    {
        //Verifica que los jugadores aún existen
        if (player1 == null && player2 == null) return null;

        if (player1 != null && player2 != null)
        {
            float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
            float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);
            return distanceToPlayer1 < distanceToPlayer2 ? player1 : player2;
        }
        else if (player1 != null)
        {
            return player1;
        }
        else if (player2 != null)
        {
            return player2;
        }
        
        return null;
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