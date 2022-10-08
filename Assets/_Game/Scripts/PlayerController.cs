using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    //Movement
    public float moveSpeed = 800f;
    public float rotateSpeed = 3f;
    public float jumpHeight = 2;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 moveDirection;
    private bool canJump;
    private int numberOfJumps;

    //Shoot
    public float fireRate;
    public Transform hand;

    private float cycleTime;


    void Awake()
    {
        controller = GetComponent<CharacterController>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Shoot.performed += Shoot;
    }
    private void Update()
    {
        Move();
    }
    private void Shoot(InputAction.CallbackContext context)
    {
        if(Time.time> cycleTime)
        {
            cycleTime = Time.time + fireRate;
            GameObject spellBall = SpellBallPool.instance.GetPooledSpellBall();
            if (spellBall != null)
            {
                spellBall.transform.position = hand.position;
                spellBall.transform.rotation = Quaternion.identity;
                spellBall.SetActive(true);
            }
        }
    }
    private void Move()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            numberOfJumps = 0;
        }

        moveDirection = new Vector3(inputVector.x, 0, 0).normalized;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            canJump = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded || numberOfJumps < 1)
        {
            canJump = true;
            numberOfJumps++;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
        }
    }

}
