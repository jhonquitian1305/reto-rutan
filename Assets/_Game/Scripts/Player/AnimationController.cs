using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class AnimationController : MonoBehaviour
{
    public Animator animatorPlayer;
    private CharMoveController charMove;

    // Start is called before the first frame update
    void Start()
    {
        animatorPlayer = GetComponent<Animator>();
        charMove = GetComponent<CharMoveController>();
    }

    private void Update()
    {
        RunAnim();
        JumpAnim();
        FallingAnim();
    }

    #region Move and Jump
    public void RunAnim()
    {
        if (charMove.isRunning)
        {
            animatorPlayer.SetBool("isRunning", true);
        }
        else if (!charMove.isRunning)
        {
            animatorPlayer.SetBool("isRunning", false);
        }
    }

    public void JumpAnim()
    {
        if (charMove.isJumping)
        {
            animatorPlayer.SetBool("isJumping", true);
        }
    }

    public void FallingAnim()
    {
        if (charMove.isFalling)
        {
            animatorPlayer.SetBool("isJumping", false);
            animatorPlayer.SetBool("isFalling", true);
        }else if (!charMove.isFalling)
        {
            animatorPlayer.SetBool("isFalling", false);
        }
    }
    #endregion

    #region Animation Attack
    public void CastAnimation()
    {
        charMove.canMove = false;
        charMove.isRunning = false;
        animatorPlayer.SetBool("isRunning", false);
        animatorPlayer.SetBool("CastAttack", true);
    }

    public void FinishCast()
    {
        animatorPlayer.SetBool("CastAttack", false);
        charMove.canMove = true;
    }

    public void CastToRun()
    {
        animatorPlayer.SetBool("CastAttack", false);
        charMove.canMove = true;
        charMove.isRunning = true;
        animatorPlayer.SetBool("isRunning", true);
    }
    #endregion
}


