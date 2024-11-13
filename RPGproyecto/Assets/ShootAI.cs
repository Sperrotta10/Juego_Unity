using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShoots = 5f;
    [SerializeField] private float attackRange = 10f; //Attack range
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    private EnemyAnimation enemyAnimation;

    private void Start()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShoots);

            Transform closestPlayer = GetClosestPlayer();
            
            if (closestPlayer != null && Vector2.Distance(transform.position, closestPlayer.position) <= attackRange)
            {
                enemyAnimation.TriggerAttack();
                yield return new WaitForSeconds(0.5f);
                ShootProjectile(closestPlayer);
            }
        }
    }

    private Transform GetClosestPlayer()
    {
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        return distanceToPlayer1 < distanceToPlayer2 ? player1 : player2;
    }

    private void ShootProjectile(Transform target)
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ProjectileFreezer projectileScript = projectileInstance.GetComponent<ProjectileFreezer>();

        if (projectileScript != null)
        {
            projectileScript.SetTarget(target);
        }
    }
}
