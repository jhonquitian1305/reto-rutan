using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    void Start()
    {
    }

    private void Awake()
    {
    }
    private void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Shoot.started += ShootPerformed;
        playerInputActions.Player.Shoot.performed += ShootPerformed;
        playerInputActions.Player.Shoot.canceled += ShootPerformed;
        playerInputActions.Player.Movement.performed += MovementPerformed;
        playerInputActions.Player.Movement.canceled += MovementPerformed;
        playerInputActions.Player.Jump.performed += JumpPerformed;
        playerInputActions.Player.Melee.started += MeleePerformed;
        playerInputActions.Player.Melee.performed += MeleePerformed;
        playerInputActions.Player.Melee.canceled += MeleePerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Shoot.started -= ShootPerformed;
        playerInputActions.Player.Shoot.performed -= ShootPerformed;
        playerInputActions.Player.Shoot.canceled -= ShootPerformed;
        playerInputActions.Player.Movement.performed -= MovementPerformed;
        playerInputActions.Player.Movement.canceled -= MovementPerformed;
        playerInputActions.Player.Jump.performed -= JumpPerformed;
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
        if (ctx.started)
        {
            GetComponent<SpellController>().EnableIndicator(true);
        }
        else if (ctx.canceled)
        {
            if (GetComponent<SpellController>().CooldownTime()<=0)
            {
                //rb.constraints = RigidbodyConstraints.FreezeAll;
                //playerAnim.AttackAnimation();
                GetComponent<SpellController>().Shoot();
                //Invoke("playerAnim.FinishAttack()", 2f);
            }
            GetComponent<SpellController>().EnableIndicator(false);
        }
    }

    public void MovementPerformed(InputAction.CallbackContext ctx)
    {
        GetComponent<MovementController>().MoveInputVector = ctx.ReadValue<Vector2>();
    }

    public void JumpPerformed(InputAction.CallbackContext ctx)
    {
        GetComponent<MovementController>().Jump();
    }
}
