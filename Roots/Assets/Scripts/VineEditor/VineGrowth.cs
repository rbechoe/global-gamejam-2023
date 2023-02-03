using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineGrowth : MonoBehaviour
{
    public LayerMask layerMask;
    public LineRenderer line1, line2, line3;
    public Collider[] colliders;

    private float minWidth = 0.005f;
    private float maxWidth = 0.25f;
    private float maxDistance = 3f;

    void Start()
    {
        colliders = Physics.OverlapSphere(transform.position, maxDistance, layerMask);

        float dist1 = float.MaxValue;
        float dist2 = float.MaxValue;
        float dist3 = float.MaxValue;
        Collider col1 = null;
        Collider col2 = null;
        Collider col3 = null;

        // Look for most nearby 3 within x distance
        foreach (Collider col in colliders)
        {
            /* first row original values, second row new values, third row (if any) final values
             * Sample system
             * dist  = 0.2
             * dist1 = 0.21 - 0.2 
             * dist2 = 0.28 - 0.2 - 0.21
             * dist3 = 0.4  - 0.2 - 0.28
             * */
            float dist = Vector3.Distance(transform.position, col.gameObject.transform.position);
            if (dist < dist3)
            {
                dist3 = dist;
                col3 = col;

                if (dist3 < dist2)
                {
                    dist3 = dist2;
                    dist2 = dist;
                    col3 = col2;
                    col2 = col;

                    if (dist2 < dist1)
                    {
                        dist2 = dist1;
                        dist1 = dist;
                        col2 = col1;
                        col1 = col;
                    }
                }
            }
        }

        // Connect nodes
        if (col1 != null)
        {
            line1.SetPosition(0, line1.gameObject.transform.position);
            line1.SetPosition(1, col1.gameObject.transform.position);
        }
        if (col2 != null)
        {
            line2.SetPosition(0, line2.gameObject.transform.position);
            line2.SetPosition(1, col2.gameObject.transform.position);
        }
        if (col3 != null)
        {
            line3.SetPosition(0, line3.gameObject.transform.position);
            line3.SetPosition(1, col3.gameObject.transform.position);
        }

        // Linewidth based on distance to next node from 0.005 to 0.25
        // connect with existing nodes, so at connect should be max width
        if (col1 != null)
        {
            float width = Mathf.Clamp(dist1 / (maxDistance * 4), minWidth, maxWidth);
            line1.startWidth = width;
            line1.endWidth = maxWidth;
        }
        if (col2 != null)
        {
            float width = Mathf.Clamp(dist2 / (maxDistance * 4), minWidth, maxWidth);
            line2.startWidth = width;
            line2.endWidth = maxWidth;
        }
        if (col3 != null)
        {
            float width = Mathf.Clamp(dist3 / (maxDistance * 4), minWidth, maxWidth);
            line3.startWidth = width;
            line3.endWidth = maxWidth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
