using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyNavMesh : MonoBehaviour
{
    public float sightRange;
    public float attackRange;
    public LayerMask playerLayer;
    public List<Transform> walkpoints;
    public bool canSummon;
    public bool canAttack;
    public bool canMove=true;
    public bool isDead;
    public IEnemyAnimController casterAnimController;

    private NavMeshAgent navMeshAgent;
    private int currentWalkpointIndex=0;
    private bool playerInSightRange, playerInAttackRange, playerInAttackSight;

    private GameObject player;
    private EnemyRangeAttack enemyRangeAttack;
    private EnemySummon enemySummon;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        casterAnimController = GetComponent<IEnemyAnimController>();

        enemyRangeAttack = GetComponent<EnemyRangeAttack>();
        enemySummon = GetComponent<EnemySummon>();
        if (enemyRangeAttack == null) canAttack = false;
        if (enemySummon == null) canSummon = false;

        if (attackRange==0) attackRange = enemyRangeAttack.spellRange - 1;
        currentWalkpointIndex = 0;
    }

    private void Update()
    {
        if (isDead)
        {
            StayOnPosition();
            return;
        }
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (canAttack) playerInAttackSight = enemyRangeAttack.PlayerInSight();
        else playerInAttackSight = true;

        if((!playerInSightRange && !playerInAttackRange) || player.GetComponent<PlayerHealthSystem>().isDead) Patroling();
        else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange && playerInSightRange)
        {
            if (playerInAttackSight) AttackPlayer();
            else ChasePlayer();
        }
        if (!canMove) StayOnPosition();
        ClampRotation();
    }
    public void StayOnPosition()
    {
        navMeshAgent.SetDestination(transform.position);
        
    }
    private void ClampRotation()
    {
        float rx = transform.eulerAngles.x;
        if (rx >= 180) rx -= 360;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(rx, -35, 35),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );
    }

    private void Patroling()
    {
        if (walkpoints.Count <= 0)
        {
            casterAnimController.Idle();
            navMeshAgent.SetDestination(transform.position);
            return;
        }
        casterAnimController.WalkAnim();
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
        casterAnimController.WalkAnim();
        navMeshAgent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
    }

    private void AttackPlayer()
    {

        Vector3 distanceToPlayer = transform.position - player.transform.position;
        if (distanceToPlayer.magnitude<4)
        {
            casterAnimController.WalkBack();
            navMeshAgent.SetDestination(transform.position - transform.forward);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
            casterAnimController.Idle();
        }
        transform.LookAt(player.transform);
        if (canAttack) enemyRangeAttack.RangeAttack();
        if (canSummon) enemySummon.CastSummonEnemy();
    }
}
