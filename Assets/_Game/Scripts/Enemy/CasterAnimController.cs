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

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();

    }

    private void Update()
    {

    }

    public void Idle()
    {
        enemyAnim.SetTrigger("Idle");
    }
    public void WalkBack()
    {
        enemyAnim.SetTrigger("WalkBack");
    }
    public void WalkAnim()
    {
        enemyAnim.SetTrigger("Walk");
    }

    public void AttackAnim()
    {
        enemyAnim.SetTrigger("Attack");
    }

    public void GetHitAnim()
    {
        enemyAnim.SetTrigger("GetHit");
    }
    public void Die()
    {
        enemyAnim.SetTrigger("Die");
    }
}


