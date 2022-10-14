using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private LineRenderer lr;
    private Transform startPoint;
    private Transform finalPoint;
    private bool canCast;

    public bool CanCast { get => canCast; set => canCast = value; }

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        startPoint = transform;
        finalPoint=GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canCast)
        {
            CastLaser();
        }
        else
        {
            lr.enabled = false;
        }
    }

    private void CastLaser()
    {
        lr.enabled = true;
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, finalPoint.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }

        }
        else
        {
            lr.SetPosition(1, finalPoint.forward * 5000);
        }
    }
}
