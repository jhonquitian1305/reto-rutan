using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody playerRigidbody;
    PlayerInputActions playerInputActions;
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
 

    }

    private void FixedUpdate()
    {
        float horizontalValue = playerInputActions.Player.Movement.ReadValue<float>();
        Vector3 movementVector = new Vector3(horizontalValue, 0, 0);
        playerRigidbody.MovePosition(transform.position + movementVector * Time.fixedDeltaTime * speed);

    }

    private void Jump(InputAction.CallbackContext context)
    {
        playerRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode.Impulse);
    }


    // Update is called once per frame

}
