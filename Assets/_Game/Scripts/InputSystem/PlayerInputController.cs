using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private AnimationController playerAnim;
    private Rigidbody rb;

    void Start()
    {
        playerAnim = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody>();
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
    }

    private void OnDisable()
    {
        playerInputActions.Player.Shoot.started -= ShootPerformed;
        playerInputActions.Player.Shoot.performed -= ShootPerformed;
        playerInputActions.Player.Shoot.canceled -= ShootPerformed;
        playerInputActions.Player.Movement.performed -= MovementPerformed;
        playerInputActions.Player.Movement.canceled -= MovementPerformed;
        playerInputActions.Player.Jump.performed -= JumpPerformed;

        playerInputActions.Player.Disable();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
    }

    public void ShootPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            GetComponent<SpellController>().EnableIndicator(true);
        }
        else if (ctx.canceled)
        {
            if (Time.time > GetComponent<SpellController>().cycleTime)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                playerAnim.AttackAnimation();
                //GetComponent<SpellController>().Shoot();
                GetComponent<SpellController>().EnableIndicator(false);
                Invoke("playerAnim.FinishAttack()", 2f);
            }
        }
    }

    public void MovementPerformed(InputAction.CallbackContext ctx)
    {

        GetComponent<MovementController>().MoveInputVector = ctx.ReadValue<Vector2>();
        if (ctx.performed)
        {
            playerAnim.RunAnim();
        }else if (ctx.canceled)
        {
            playerAnim.StopRun();
        }
    }

    public void JumpPerformed(InputAction.CallbackContext ctx)
    {
        GetComponent<MovementController>().Jump();
    }
}
