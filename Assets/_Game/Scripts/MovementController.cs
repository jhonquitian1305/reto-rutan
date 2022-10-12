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
    public Rigidbody rigidBody;
    public bool isGrounded;
    private bool canDoubleJump;
    PlayerInputController inputController;
    public Vector2 MoveInputVector { get => moveInputVector; set => moveInputVector = value; }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        inputController = GetComponent<PlayerInputController>();
    }
    void FixedUpdate()
    {
        CheckIfGrounded();
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

    private void CheckIfGrounded()
    {
        RaycastHit[] hits;

        float distance = (GetComponent<CapsuleCollider>().height / 2);
        Vector2 positionToCheck = transform.position;
        hits = Physics.RaycastAll(positionToCheck, Vector3.down, distance);

        //if a collider was hit, we are grounded
        if (hits.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
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
