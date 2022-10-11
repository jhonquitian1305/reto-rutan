using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public float rotationSpeed = 10f;

    private Vector2 moveInputVector;
    private Rigidbody rigidBody;
    private bool isGrounded;
    private bool canDoubleJump;

    public Vector2 MoveInputVector { get => moveInputVector; set => moveInputVector = value; }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
        Rotate();
    }
    public void Move()
    {
        rigidBody.velocity = new Vector3(moveInputVector.x * moveSpeed, rigidBody.velocity.y);
        
    }

    private void Rotate()
    {
        if (moveInputVector.x != 0)
        {
            Quaternion currentRotation = rigidBody.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(moveInputVector);
            Quaternion newRotation = Quaternion.Slerp(
                currentRotation, // mix where the rig points now
                targetRotation,  // with where it should point
                rotationSpeed * Time.fixedDeltaTime); // with this ratio
            rigidBody.MoveRotation(newRotation);
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer==3)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }

}