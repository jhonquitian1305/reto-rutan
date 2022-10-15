using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public Transform target;
    public bool enemyMove;
    private NavMeshAgent navEnemy;

    void Awake()
    {
        navEnemy = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            navEnemy.destination = target.position;
            navEnemy.isStopped = false;
            enemyMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            navEnemy.isStopped = true;
            enemyMove = false;
        }
    }
}
