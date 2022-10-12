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
    public bool isGrounded;
    private bool canDoubleJump;
    PlayerInputController inputController;
    public Vector2 MoveInputVector { get => moveInputVector; set => moveInputVector = value; }
    public Rigidbody RigidBody { get => rigidBody; set => rigidBody = value; }
    public bool CanDoubleJump { get => canDoubleJump; set => canDoubleJump = value; }

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
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
        RigidBody.velocity = new Vector3(moveInputVector.x * moveSpeed, RigidBody.velocity.y);
        
    }

    private void Rotate()
    {
        if (moveInputVector.x != 0)
        {
            Quaternion currentRotation = RigidBody.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(moveInputVector);
            Quaternion newRotation = Quaternion.Slerp(
                currentRotation, // mix where the rig points now
                targetRotation,  // with where it should point
                rotationSpeed * Time.fixedDeltaTime); // with this ratio
            RigidBody.MoveRotation(newRotation);
        }
    }
    public void Jump()
    {
        if (isGrounded)
        {
            RigidBody.velocity = new Vector3(moveInputVector.x * moveSpeed, jumpForce);

            CanDoubleJump = true;
        }
        else if (CanDoubleJump)
        {
            RigidBody.velocity = Vector3.up * jumpForce;
            CanDoubleJump = false;
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
