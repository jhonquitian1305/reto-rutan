using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyNavMesh : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform player;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public LayerMask playerLayer;

    private EnemyRangeAttack enemyRangeAttack;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyRangeAttack = GetComponent<EnemyRangeAttack>();

        attackRange = enemyRangeAttack.spellRange;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(player);
        enemyRangeAttack.RangeAttack();
    }
}
