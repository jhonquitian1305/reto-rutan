using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MeleeEnemyNavMesh : MonoBehaviour
{
    public float sightRange=10;
    public float attackRange=1;
    public LayerMask playerLayer;
    public List<Transform> walkpoints;
    public bool canMove = true;
    public bool isDead;

    public IEnemyAnimController meleeAnimController;
    private NavMeshAgent navMeshAgent;
    private int currentWalkpointIndex = 0;
    private bool playerInSightRange, playerInAttackRange;
    private Transform player;
    private EnemyMeleeAttack enemyMeleeAttack;

    private void Awake()
    {
        canMove = true;
        player = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        meleeAnimController = GetComponent<IEnemyAnimController>();

        enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
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


        if ((!playerInSightRange && !playerInAttackRange) || player.GetComponent<PlayerHealthSystem>().isDead) Patroling();
        else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange && playerInSightRange) AttackPlayer();
        if (!canMove) StayOnPosition();
        ClampRotation();
    }

    private void Patroling()
    {
        if (walkpoints.Count <= 0)
        {
            meleeAnimController.Idle();
            return;
        }
        meleeAnimController.WalkAnim();
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
        meleeAnimController.WalkAnim();
        Vector3 lookAtVector = player.position;
        lookAtVector.y = transform.position.y;
        transform.LookAt(lookAtVector);
        navMeshAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        meleeAnimController.Idle();
        navMeshAgent.SetDestination(transform.position);
        Vector3 lookAtVector = player.position;
        lookAtVector.y = transform.position.y;
        transform.LookAt(lookAtVector);
        enemyMeleeAttack.MeleeAttack();
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
    public void StayOnPosition()
    {
        navMeshAgent.SetDestination(transform.position);

    }
}
