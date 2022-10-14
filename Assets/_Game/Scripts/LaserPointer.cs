using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserPointer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public LayerMask layerMask;
    public Vector2 laserOffset=new(0.3f, 0.24f);
    public float range = 10;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.01f;
        EnableLaser(false);
    }

    // Update is called once per frame
    void Update()
    {
        DrawLaser();
    }

    public void EnableLaser(bool enabled)
    {
        lineRenderer.enabled = enabled;
    }

    private void DrawLaser()
    {
        Vector3 startPosition = transform.position;
        startPosition += transform.forward * laserOffset.x;
        startPosition += transform.right * laserOffset.y;
        startPosition.y += 0.05f;

        Vector3 finalDirection = transform.forward;

        lineRenderer.SetPosition(0, startPosition);

        RaycastHit hit;
        if (Physics.Raycast(startPosition, finalDirection, out hit, range, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

        }
        else
        {
            lineRenderer.SetPosition(1, finalDirection*range*1.1f+startPosition);
        }

    }
}
