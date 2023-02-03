using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineGrowth : MonoBehaviour
{
    public LayerMask layerMask;
    public LineRenderer line1, line2, line3;
    public Collider[] colliders;

    void Start()
    {
        colliders = Physics.OverlapSphere(transform.position, 5, layerMask);

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


        // TODO linewidth based on distance to next node from 0.1 to 0.25
        // TODO upon recalculate readjust vines

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
