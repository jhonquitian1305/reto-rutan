using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class AnimationController : MonoBehaviour
{
    public Animator animatorPlayer;
    private CharMoveController charMove;
    private MeleeController meleeController;
    private int meleeCount;
    // Start is called before the first frame update
    void Start()
    {
        animatorPlayer = GetComponent<Animator>();
        charMove = GetComponent<CharMoveController>();
        meleeController = GetComponentInChildren<MeleeController>();
    }

    private void Update()
    {
        //RunAnim();
        //JumpAnim();
        //FallingAnim();
        GlobalAnimatorController();
    }

    private void GlobalAnimatorController()
    {
        if (!charMove.lockCamera)
        {
            StopLockMove();
            RunAnim();
            JumpAnim();
            FallingAnim();
        }
        else if (charMove.lockCamera)
        {
            MoveWhenLocked();
            FallingAnim();
            FinishMelee();
        }
    }
    public void HitAnim()
    {
        animatorPlayer.SetTrigger("Hit");
        charMove.canMove = false;
        animatorPlayer.SetBool("CastAttack", false);
        FinishMelee();
    }

    public void EndHitAnim()
    {
        charMove.canMove = true;
    }

    public void DieAnim()
    {
        animatorPlayer.SetTrigger("Die");
        charMove.canMove = false;
    }

    #region UnlockMove
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
        if (charMove.isJumping & !charMove.lockCamera)
        {
            animatorPlayer.SetBool("isJumping", true);
        }
        else if (!charMove.isJumping)
        {
            animatorPlayer.SetBool("isJumping", false);
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

    #region LockMove
    private void MoveWhenLocked()
    {
        animatorPlayer.SetBool("isRunning", false);
        switch (charMove.lateralMove)
        {
            case 1:
                animatorPlayer.SetBool("WalkLeft", true);
                animatorPlayer.SetBool("WalkRight", false);

                break;
            case 2:
                animatorPlayer.SetBool("WalkRight", true);
                animatorPlayer.SetBool("WalkLeft", false);

                break;
            default:
                animatorPlayer.SetBool("WalkRight", false);
                animatorPlayer.SetBool("WalkLeft", false);
                break;
        }

        switch (charMove.forwardMove)
        {
            case 1:
                animatorPlayer.SetBool("WalkBack", false);
                animatorPlayer.SetBool("WalkForward", true);

                break;
            case 2:
                animatorPlayer.SetBool("WalkForward", false);
                animatorPlayer.SetBool("WalkBack", true);

                break;
            default:
                animatorPlayer.SetBool("WalkForward", false);
                animatorPlayer.SetBool("WalkBack", false);
                break;
        }
    }

    private void StopLockMove()
    {
        animatorPlayer.SetBool("WalkLeft", false);
        animatorPlayer.SetBool("WalkRight", false);
        animatorPlayer.SetBool("WalkForward", false);
        animatorPlayer.SetBool("WalkBack", false);
    }

    #endregion

    #region Animation Attack
    public void CastAnimation()
    {
        if (charMove.isGrounded&&!animatorPlayer.GetBool("isAttacking"))
        {
            charMove.canMove = false;
            charMove.isRunning = false;
            animatorPlayer.SetBool("isRunning", false);
            animatorPlayer.SetBool("CastAttack", true);
        }
    }

    public void FinishCast()
    {
        animatorPlayer.SetBool("CastAttack", false);
        charMove.canMove = true;
        meleeCount = 0;
    }

    public void CastToRun()
    {
        animatorPlayer.SetBool("isRunning", true);
        animatorPlayer.SetBool("CastAttack", false);
        charMove.canMove = true;
        charMove.isRunning = true;
        meleeCount = 0;
    }

    public void MeleeAttack()
    {
        if (!charMove.isGrounded) return; 
        meleeCount++;
        if (meleeCount == 1) {
            charMove.isSlowed = true;
            animatorPlayer.SetBool("isAttacking", true);
            //animatorPlayer.SetTrigger("Melee");
        } 
    }

    public void CheckIfCombo(int attackNumber)
    {
        if(meleeCount < attackNumber)
        {
            FinishMelee();
            RestartMeleeCooldown();
        }
        else if (attackNumber == 2)
        {
            meleeController.bonusMultiplier = 1.15f;
        } else if (attackNumber == 3)
        {
            meleeController.bonusMultiplier = 1.3f;
        }
    }

    public void RestartMeleeCooldown()
    {
        meleeController.RestartCooldown();
    }

    public void FinishMelee()
    {
        meleeController.bonusMultiplier = 1;
        meleeCount = 0;
        charMove.isSlowed = false;
        animatorPlayer.SetBool("isAttacking", false);  
    }
    #endregion

    public void PlayMeleeSound()
    {
        //FindObjectOfType<SoundManager>().Play("Melee");
    }
}


