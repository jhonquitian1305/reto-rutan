using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyNavMesh : MonoBehaviour
{
    public float sightRange;
    public LayerMask playerLayer;
    public List<Transform> walkpoints;

    private NavMeshAgent navMeshAgent;
    private int currentWalkpointIndex=0;
    private bool playerInSightRange, playerInAttackRange, playerInAttackSight;
    private float attackRange;
    private Transform player;
    private EnemyRangeAttack enemyRangeAttack;
    
    [SerializeField] private AudioSource enemyAttackSoundEffect;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyRangeAttack = GetComponent<EnemyRangeAttack>();

        attackRange = enemyRangeAttack.spellRange;
        currentWalkpointIndex = 0;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        playerInAttackSight = enemyRangeAttack.PlayerInSight();
        
        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
        {
            if(playerInAttackSight) AttackPlayer();
            else ChasePlayer();
        }
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
                Vector3 distanceToPlayer = transform.position - player.position;
                navMeshAgent.SetDestination(player.position);
                transform.LookAt(player);
            }

            private void AttackPlayer()
            {
                //Make sure enemy doesn't move
                navMeshAgent.SetDestination(transform.position);
                transform.LookAt(player);
                enemyRangeAttack.RangeAttack();
                enemyAttackSoundEffect.Play();
            }
}
