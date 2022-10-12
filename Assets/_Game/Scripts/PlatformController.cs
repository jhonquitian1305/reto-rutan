using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject[] waypoints;
    public float speed = 2f;

    private int waypointsIndex = 0;

    private Vector3 playerScale;
    private Vector3 prevPos;
    private float currentVelocityY;
    private float charOnPlatformJumpForce;

    void OnEnable()
    {
        playerScale = GameObject.FindWithTag("Player").transform.localScale;
    }

    void FixedUpdate()
    {
        SetCurrentVelocityY();
        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector3 platform = transform.position;
        Vector3 waypointTarget = waypoints[waypointsIndex].transform.position;
        float platformSpeed = speed*Time.deltaTime;
        if (Vector3.Distance(platform, waypointTarget) < 0.9f)
        {
            waypointsIndex++;
            if (waypointsIndex >= waypoints.Length)
            {
                waypointsIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(platform, waypointTarget, platformSpeed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.transform.parent = transform;
            charOnPlatformJumpForce = collider.GetComponent<MovementController>().jumpForce; 
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<MovementController>().jumpForce = charOnPlatformJumpForce + GetCurrentVelocityY();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.transform.parent = null;
            collider.transform.localScale = playerScale;
            collider.GetComponent<MovementController>().jumpForce = charOnPlatformJumpForce;
        }
    }
    private void SetCurrentVelocityY()
    {
        currentVelocityY = ((transform.position - prevPos) / Time.fixedDeltaTime).y;
        prevPos = transform.position;
    }
    private float GetCurrentVelocityY()
    {
        if (currentVelocityY > 0)
        {
            return currentVelocityY;
        }
        else
        {
            return 0;
        }
    }
}
