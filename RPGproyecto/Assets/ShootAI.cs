using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShoots;
    
    void Start()
    {
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {   
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShoots);
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}
