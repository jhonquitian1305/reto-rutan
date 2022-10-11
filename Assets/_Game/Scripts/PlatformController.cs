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
    void OnEnable()
    {
        playerScale = GameObject.FindWithTag("Player").transform.localScale;
    }

    void FixedUpdate()
    {
        MovePlatform();
    }

    void MovePlatform()
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
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.transform.parent = null;
            collider.transform.localScale = playerScale;
        }
    }
}
