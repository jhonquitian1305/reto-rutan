using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public float rotationSpeed = 10f;
    public Transform groundCheck;
    public LayerMask ground;

    private Vector2 moveInputVector;
    private Rigidbody rigidBody;
    private bool isGrounded;
    private bool canDoubleJump;
    private Transform cameraTransform;
    private Vector3 moveVector;

    public Vector2 MoveInputVector { get => moveInputVector; set => moveInputVector = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }
    void FixedUpdate()
    {
        CheckIfGrounded();
        Move();
        Rotate();
        Debug.Log(isGrounded);
    }
    public void Move()
    {
        moveVector = new Vector3(moveInputVector.x * moveSpeed, rigidBody.velocity.y, moveInputVector.y * moveSpeed);

        moveVector = moveVector.x * cameraTransform.right.normalized + moveVector.z * cameraTransform.forward.normalized;
        moveVector.y = rigidBody.velocity.y;

        rigidBody.velocity = moveVector;
    }

    private void Rotate()
    {
        Vector3 targetVector = moveVector;
        targetVector.y = 0;

        if (targetVector != Vector3.zero)
        {
            rigidBody.transform.rotation = Quaternion.Slerp(rigidBody.transform.rotation, Quaternion.LookRotation(targetVector), Time.deltaTime * rotationSpeed);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rigidBody.velocity = new Vector3(moveInputVector.x * moveSpeed, jumpForce);

            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            rigidBody.velocity = Vector3.up * jumpForce;
            canDoubleJump = false;
        }
    }

    private void CheckIfGrounded()
    {
        if(Physics.CheckSphere(groundCheck.position, .1f, ground))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.transform.CompareTag("Ground"))
        //{
        //    isGrounded = true;
        //    Debug.Log("Grounded");
        //}
        if (other.gameObject.transform.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.transform.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //        Debug.Log("Not Grounded");
    //    }
        
    //}



}
