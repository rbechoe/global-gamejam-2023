using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineConnector : MonoBehaviour
{
    public LayerMask layerMask;
    public LineRenderer line1;
    public Collider[] colliders;

    // TODO each branch becomes smaller
    // TODO when too small, branch dies
    private float minWidth = 0.005f;
    private float maxWidth = 0.25f;
    private float maxDistance = 3f;

    public Vector3 direction; // TODO decides which direction the vine will grow to
    public float weight; // TODO weight is used in order to decide whether a branch will be connected, decreases each time unless it scores highest of the bunch

    void Start()
    {
        // Cast raycast in each direction to snap block to surface
        List<Vector3> points = new List<Vector3>();
        points.Add(GetCollisionPoint(Vector3.forward));
        points.Add(GetCollisionPoint(Vector3.back));
        points.Add(GetCollisionPoint(Vector3.right));
        points.Add(GetCollisionPoint(Vector3.left));
        points.Add(GetCollisionPoint(Vector3.up));
        points.Add(GetCollisionPoint(Vector3.down));
        float closestDist = float.MaxValue;
        Vector3 hitPoint = Vector3.zero;

        // Snap to closest distance
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i] == Vector3.zero) continue;

            float dist = Vector3.Distance(transform.position, points[i]);
            if (dist < closestDist)
            {
                closestDist = dist;
                hitPoint = points[i];
            }
        }
        transform.position = hitPoint;

        colliders = Physics.OverlapSphere(transform.position, maxDistance, layerMask);

        float dist1 = float.MaxValue;
        Collider col1 = null;

        // Look for most nearby 3 within x distance
        foreach (Collider col in colliders)
        {
            float dist = Vector3.Distance(transform.position, col.gameObject.transform.position);
            if (dist < dist1)
            {
                dist1 = dist;
                col1 = col;
            }
        }

        // Connect nodes
        if (col1 != null)
        {
            line1.SetPosition(0, line1.gameObject.transform.position);
            line1.SetPosition(1, col1.gameObject.transform.position);
        }

        // Linewidth based on distance to next node from 0.005 to 0.25
        // connect with existing nodes, so at connect should be max width
        if (col1 != null)
        {
            float width = Mathf.Clamp(dist1 / (maxDistance * 4), minWidth, maxWidth);
            line1.startWidth = width;
            line1.endWidth = maxWidth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetCollisionPoint(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 5))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
