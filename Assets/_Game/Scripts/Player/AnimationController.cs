using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animatorPlayer;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        animatorPlayer = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    #region Move and Jump
    public void RunAnim()
    {
        animatorPlayer.SetBool("run", true);
        animatorPlayer.SetBool("Jump", false);
        animatorPlayer.SetBool("shoot", false);
    }

    public void StopRun()
    {
        animatorPlayer.SetBool("run", false);
    }

    public void JumpAnim()
    {
        animatorPlayer.SetBool("Jump", true);
    }

    public void ResetJump()
    {
        animatorPlayer.SetBool("Jump", false);
    }

    #endregion

    #region Animation Attack
    public void AttackAnimation()
    {
        animatorPlayer.SetBool("shoot", true);
        animatorPlayer.SetBool("run", false);
        //animatorPlayer.SetBool("Jump", false);
    }

    public void FinishAttack()
    {
        animatorPlayer.SetBool("shoot", false);
    }

    public void DeniedAttack()
    {
        
    }
    #endregion

    #region Constraints Control

    public void UnfreezeMove()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    #endregion
}