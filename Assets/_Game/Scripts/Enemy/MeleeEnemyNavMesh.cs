using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MeleeEnemyNavMesh : MonoBehaviour
{
    public float sightRange;
    public LayerMask playerLayer;
    public List<Transform> walkpoints;

    private NavMeshAgent navMeshAgent;
    private int currentWalkpointIndex = 0;
    private bool playerInSightRange;
    private Transform player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentWalkpointIndex = 0;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        if (!playerInSightRange) Patroling();
        else if (playerInSightRange) ChasePlayer();
        
    }

    private void Patroling()
    {
        Vector3 distanceToWalkPoint = transform.position - walkpoints[currentWalkpointIndex].position;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            if (currentWalkpointIndex < walkpoints.Count - 1)
            {
                currentWalkpointIndex++;
            }
            else
            {
                currentWalkpointIndex = 0;
            }
        }
        navMeshAgent.SetDestination(walkpoints[currentWalkpointIndex].position);

    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }
}
