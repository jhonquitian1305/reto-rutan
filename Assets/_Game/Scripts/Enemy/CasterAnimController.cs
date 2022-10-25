using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class CasterAnimController : MonoBehaviour
{
    public Animator enemyAnim;
    private RangeEnemyNavMesh enemyNavMesh;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyNavMesh = GetComponent<RangeEnemyNavMesh>();
    }

    private void Update()
    {

    }

    public void WalkBack()
    {
        enemyAnim.SetBool("isWalkingBack", true);
    }
    public void WalkAnim()
    {
        enemyAnim.SetBool("isWalking", true);
    }

    public void Idle()
    {
        enemyAnim.SetBool("isAttacking", false);
        enemyAnim.SetBool("isWalking", false);
        enemyAnim.SetBool("isWalkingBack", false);
    }

    public void AttackAnim()
    {
        enemyAnim.SetBool("isAttacking", true);
        enemyAnim.SetBool("isWalking", false);
        enemyAnim.SetBool("isWalkingBack", false);
        enemyNavMesh.canMove = false;
    }
    public void EndAttackAnim()
    {
        enemyNavMesh.canMove = true;
        enemyAnim.SetBool("isAttacking", false);

    }

    public void GetHitAnim()
    {
        enemyAnim.SetTrigger("GetHit");
        enemyAnim.SetBool("isWalking", false);
        enemyAnim.SetBool("isWalkingBack", false);
        enemyAnim.SetBool("isAttacking", false);
        enemyNavMesh.canMove = false;
    }

    public void EndGetHitAnim()
    {
        enemyNavMesh.canMove = true;
    }
    public void DieAnim()
    {
        enemyAnim.SetTrigger("Die");
        enemyNavMesh.isDead=true;
    }
}


