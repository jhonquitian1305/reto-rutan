using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Shoot.performed += ShootPerformed;
        playerInputActions.Player.Shoot.canceled += ShootPerformed;
        playerInputActions.Player.Movement.performed += MovementPerformed;
        playerInputActions.Player.Movement.canceled += MovementPerformed;
        playerInputActions.Player.Jump.performed += JumpPerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Shoot.performed -= ShootPerformed;
        playerInputActions.Player.Shoot.canceled -= ShootPerformed;
        playerInputActions.Player.Movement.performed -= MovementPerformed;
        playerInputActions.Player.Movement.canceled -= MovementPerformed;
        playerInputActions.Player.Jump.performed -= JumpPerformed;

        playerInputActions.Player.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        FreezePosition();
        UnFreezePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MovementController>().IsGrounded)
        {
            //anim.SetBool("jump", false);
        }
    }

    public void ShootPerformed(InputAction.CallbackContext ctx)
    {
            if (ctx.canceled)
            {
                anim.SetBool("shoot", false);
            }
            else if (ctx.performed)
            {
                anim.SetBool("run", false);
                anim.SetBool("shoot", true);
                anim.SetBool("jump", false);
            }
    }

    public void MovementPerformed(InputAction.CallbackContext ctx)
    {
        
        GetComponent<MovementController>().MoveInputVector = ctx.ReadValue<Vector2>();
        if (!GameObject.FindWithTag("Player").GetComponent<MovementController>().IsGrounded)
        {
            return;
        }
        if (ctx.canceled)
        {
            anim.SetBool("run", false);
        }
        if (ctx.performed)
        {
            anim.SetBool("run", true);
            anim.SetBool("shoot", false);
            //anim.SetBool("jump", false);
        }
    }

    public void JumpPerformed(InputAction.CallbackContext ctx)
    {
        //if (ctx.canceled)
        //{
        //    anim.SetBool("jump", false);
        //}

        if (ctx.performed)
        {
            anim.SetBool("run", false);
            anim.SetBool("shoot", false);
            anim.SetBool("jump", true);
        }
        GetComponent<MovementController>().Jump();
    }

    IEnumerator WaitForShoot()
    {

        yield return new WaitForSeconds(0.5f);
        GetComponent<SpellController>().Shoot();
    }
    public void UnFreezePosition()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
    public void FreezePosition()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
