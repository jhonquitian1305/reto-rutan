using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Shoot.performed += ShootPerformed;
        playerInputActions.Player.Movement.performed += MovementPerformed;
        playerInputActions.Player.Movement.canceled += MovementPerformed;
        playerInputActions.Player.Jump.performed += JumpPerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Shoot.performed -= ShootPerformed;
        playerInputActions.Player.Movement.performed -= MovementPerformed;
        playerInputActions.Player.Movement.canceled -= MovementPerformed;
        playerInputActions.Player.Jump.performed -= JumpPerformed;

        playerInputActions.Player.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootPerformed(InputAction.CallbackContext ctx)
    {
        GetComponent<SpellController>().Shoot();
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
