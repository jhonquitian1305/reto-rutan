using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveController : MonoBehaviour
    {
    [Header("Parametros movimiento")]
    public float runningSpeed = 5;
    public float walkingSpeed = 3;
    private float gravityValue = -9.81f;
    public float jumpHeight = 0.5f;
    public bool lockCamera;
    public int lateralMove;
    public int forwardMove;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Actual Player Status")]
    public bool isRunning;
    public bool isSlowed;
    public bool canMove;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isGrounded;

    private Vector2 moveInputVector;
    public Vector3 moveVector;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Transform cameraTransform;

    public Vector2 MoveInputVector { get => moveInputVector; set => moveInputVector = value; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        canMove = true;
    }

    void Update()
    {
            CheckIfGrounded();
            Move();
            Rotate();
            GravityAction();
            CheckIfFalling();
    }

    private void Move()
    {
        if (canMove)
        {
            float speed;
            if (isSlowed||lockCamera) speed = walkingSpeed;
            else speed = runningSpeed;

            moveVector = new Vector3(moveInputVector.x, 0, moveInputVector.y);
            moveVector = moveVector.x * cameraTransform.right.normalized + moveVector.z * cameraTransform.forward.normalized;
            moveVector.y = 0;
            controller.Move(moveVector * Time.deltaTime * speed);

            if (!lockCamera)
            {
                if (moveInputVector != Vector2.zero)
                {
                    isRunning = true;
                }
                else
                {
                    isRunning = false;
                }
            }
            else if (lockCamera)
            {

                if (moveInputVector.x < 0 && (moveInputVector.y < 0.5 && moveInputVector.y > -0.5))
                {
                    lateralMove = 1;
                    forwardMove = 0;
                }
                else if (moveInputVector.x > 0 && (moveInputVector.y < 0.5 && moveInputVector.y > -0.5))
                {
                    lateralMove = 2;
                    forwardMove = 0;
                }
                else if (moveInputVector.y > 0)
                {
                    forwardMove = 1;
                    lateralMove = 0;
                }
                else if (moveInputVector.y < 0)
                {
                    forwardMove = 2;
                    lateralMove = 0;
                }
                else if (moveInputVector == Vector2.zero)
                {
                    Debug.Log("a");
                    lateralMove = 0;
                    forwardMove = 0;
                }
            }

        }       
    }
    public void Jump()
    {
        if (isGrounded && canMove && !lockCamera)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    private void GravityAction()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Rotate()
    {
        if (!lockCamera)
        {
            Vector3 targetVector = moveVector;
            targetVector.y = 0;
            if (targetVector != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetVector), Time.deltaTime * 10);
            }
        }
        else
        {
            //Rotate towards camera direction
            float targetAngle = cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1);
        }
    }


    private void CheckIfGrounded()
    {
        if (Physics.CheckSphere(groundCheck.position, .3f, groundLayer))
        {
            isGrounded = true;
            isJumping = false;
            isFalling = false;
            if (Physics.CheckSphere(groundCheck.position, .03f, groundLayer) && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
        }
        else
        {
            isGrounded = false;
            isJumping = true;    
        }
    }
    private void CheckIfFalling()
    {
        if (playerVelocity.y < 0 && !isGrounded)
        {
            isFalling = true;
        }
        else
        {
            isFalling= false;   
        }

    }

}
