using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserPointer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public LayerMask layerMask;
    public Vector3 laserOffset = new Vector3(1,1,1);

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;

    }

    // Update is called once per frame
    void Update()
    {
        DrawLaser();
    }

    private void DrawLaser()
    {
        Vector3 startPosition = transform.position;
        startPosition.y += 0.05f;

        Vector3 finalDirection = transform.forward;
        lineRenderer.SetPosition(0, startPosition);
        RaycastHit hit;
        if (Physics.Raycast(startPosition, finalDirection, out hit, layerMask, 5000, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

        }
        else
        {
            lineRenderer.SetPosition(1, finalDirection * 5000);
        }
    }
}
