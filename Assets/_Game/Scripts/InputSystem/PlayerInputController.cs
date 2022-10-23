using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    //private AnimationController playerAnimController;
    private CharMoveController playerMoveController;
    private SpellController playerSpellController;


    private void OnEnable()
    {
        //playerAnimController = GetComponent<AnimationController>();
        playerMoveController = GetComponent<CharMoveController>();
        playerSpellController = GetComponent<SpellController>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.LockCam.performed += LockCamera;

        playerInputActions.Player.Aim.started += Aim;
        playerInputActions.Player.Aim.canceled += Aim;

        playerInputActions.Player.Shoot.started += ShootPerformed;
        playerInputActions.Player.Shoot.performed += ShootPerformed;
        playerInputActions.Player.Shoot.canceled += ShootPerformed;

        playerInputActions.Player.Movement.performed += MovementPerformed;
        playerInputActions.Player.Movement.canceled += MovementPerformed;

        playerInputActions.Player.Jump.performed += JumpPerformed;

        playerInputActions.Player.ChangeToNextSpell.performed += ChangeToNextSpell;
        playerInputActions.Player.ChangeToLastSpell.performed += ChangeToLastSpell;

        playerInputActions.Player.Melee.started += MeleePerformed;
        playerInputActions.Player.Melee.performed += MeleePerformed;
        playerInputActions.Player.Melee.canceled += MeleePerformed;
    }


    private void OnDisable()
    {
        playerInputActions.Player.Aim.started -= Aim;
        playerInputActions.Player.Aim.canceled -= Aim;
        playerInputActions.Player.Shoot.started -= ShootPerformed;
        playerInputActions.Player.Shoot.performed -= ShootPerformed;
        playerInputActions.Player.Shoot.canceled -= ShootPerformed;
        playerInputActions.Player.Movement.performed -= MovementPerformed;
        playerInputActions.Player.Movement.canceled -= MovementPerformed;
        playerInputActions.Player.Jump.performed -= JumpPerformed;
        playerInputActions.Player.ChangeToNextSpell.performed -= ChangeToNextSpell;
        playerInputActions.Player.ChangeToLastSpell.performed -= ChangeToLastSpell;
        playerInputActions.Player.Melee.started -= MeleePerformed;
        playerInputActions.Player.Melee.performed -= MeleePerformed;
        playerInputActions.Player.Melee.canceled -= MeleePerformed;

        playerInputActions.Player.Disable();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeToNextSpell(InputAction.CallbackContext ctx)
    {
        playerSpellController.SetNextSpellAsActive();
    }

    public void ChangeToLastSpell(InputAction.CallbackContext ctx)
    {
        playerSpellController.SetLastSpellAsActive();
    }

    public void MeleePerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {

        }
        else if (ctx.performed)
        {

        }
        else if (ctx.canceled)
        {

        }
    }

    public void ShootPerformed(InputAction.CallbackContext ctx)
    {

        if (ctx.performed)
        {
            if (playerSpellController.CooldownTime()<=0)
            {
                playerSpellController.Shoot();
            }
        }
    }

    public void Aim(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerSpellController.EnableIndicator(true);
        }else if (ctx.canceled)
        {
            playerSpellController.EnableIndicator(false);
        }
    }

    public void LockCamera(InputAction.CallbackContext ctx)
    {
        playerMoveController.lockCamera = !playerMoveController.lockCamera;
    }

    public void MovementPerformed(InputAction.CallbackContext ctx)
    {
        playerMoveController.MoveInputVector = ctx.ReadValue<Vector2>();
    }

    public void JumpPerformed(InputAction.CallbackContext ctx)
    {
        playerMoveController.Jump();
    }
}
