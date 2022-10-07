using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpHeight = 1;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    PlayerInputActions playerInputActions;
    private bool canJump;
    private int numberOfJumps;
    void Awake()
    {
        controller = GetComponent<CharacterController>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
 

    }

    private void Update()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            numberOfJumps = 0;
        }
        Vector3 moveVector = new Vector3(inputVector.x, 0, 0);
        controller.Move(moveVector * speed * Time.deltaTime);

        if(canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            canJump = false;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);

    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(numberOfJumps);
        if (controller.isGrounded || numberOfJumps<1)
        {
            Debug.Log("Jump");
            canJump = true;
            numberOfJumps++;
        }
       
    }


    // Update is called once per frame

}
