using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class VineConnector : MonoBehaviour
{
    public LayerMask layerMask;
    public LayerMask forceMask;
    public LineRenderer line1, line2, line3;
    public Collider[] colliders;
    public GameObject AINode;

    private float minWidth = 0.05f;
    private float maxWidth = 0.2f;
    private float maxDistance = 10f;

    public float weight; // TODO weight is used in order to decide whether a branch will be connected, decreases each time unless it scores highest of the bunch

    public GameObject endPoint;
    public Vector3 endPosition;
    private Vector3 targetDir;
    private Collider conNode;

    private float lifeTime = .5f;
    private float vineCD = 0;
    private bool didRecalc = false;
    private bool holyNode = false;
    private bool reachedEnding;
    private bool cantSpawn = false;

    private void OnEnable()
    {
        EventSystem.AddListener(EventType.ReachedEnding, ReachedEnding);
    }

    private void OnDisable()
    {
        EventSystem.RemoveListener(EventType.ReachedEnding, ReachedEnding);
    }

    private void ReachedEnding()
    {
        if (reachedEnding) return;

        reachedEnding = true;
        RecalculateRoots();
    }

    void Start()
    {
        // TODO weight affects glow!!
        if (!holyNode)
        {
            endPosition = new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5),
                                    Random.Range(transform.position.y - 5, transform.position.y + 5),
                                    Random.Range(transform.position.z - 5, transform.position.z + 5));

            if (endPosition.y < 1)
            {
                endPosition = new Vector3(endPosition.x, 1, endPosition.z);
            }
            line1.startWidth = minWidth + (weight / 800f);
            line1.endWidth = minWidth + (weight / 800f);
            line2.startWidth = minWidth + (weight / 800f);
            line2.endWidth = minWidth + (weight / 800f);
            line3.startWidth = minWidth + (weight / 800f);
            line3.endWidth = minWidth + (weight / 800f);
        }
        else
        {
            endPosition = endPoint.transform.position;
            weight = 100;
            line1.startWidth = maxWidth;
            line1.endWidth = maxWidth;
            line2.startWidth = maxWidth;
            line2.endWidth = maxWidth;
            line3.startWidth = maxWidth;
            line3.endWidth = maxWidth;
        }

        if (transform.position.y < 1)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }

        if (weight <= 0)
        {
            cantSpawn = true;
        }

        lifeTime = 2f;

        ForceToSurface();
        RecalculateRoots();
    }

    // Update is called once per frame
    void Update()
    {
        if (reachedEnding) return;

        if (lifeTime >= 0)
        {
            lifeTime -= Time.deltaTime;

            targetDir = endPosition - transform.position;
            float step = Time.deltaTime * 5;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.position += transform.forward * 2 * Time.deltaTime;

            // Connect nodes
            if (conNode != null)
            {
                line1.SetPosition(0, line1.gameObject.transform.position);
                line1.SetPosition(1, conNode.gameObject.transform.position);
            }

            ForceToSurface();

            if (!cantSpawn && Random.Range(weight, 100) > 25 && vineCD <= 0 && !holyNode)
            {
                weight -= 15;
                SpawnNodes();
                vineCD = 0.5f;
            }

            if (holyNode && Vector3.Distance(transform.position, endPoint.transform.position) < 1f)
            {
                EventSystem.InvokeEvent(EventType.ReachedEnding);
                print("Reached ending");
            }
        }

        if (lifeTime <= 0 && !didRecalc)
        {
            if (holyNode)
            {
                SpawnNodes();
            }

            didRecalc = true;
            RecalculateRoots();
        }

        if (vineCD >= 0) vineCD -= Time.deltaTime;
    }

    private bool createdHolyNode = false;
    void SpawnNodes(bool extra = false)
    {
        GameObject newNode = Instantiate(AINode, transform.position, Quaternion.identity);
        newNode.transform.eulerAngles += new Vector3(Random.Range(0, 30) - 15, Random.Range(0, 30) - 15, Random.Range(0, 30) - 15);
        VineConnector nodeVC = newNode.GetComponent<VineConnector>();
        nodeVC.endPoint = endPoint;
        nodeVC.weight = weight - 15;
        newNode.name = "Node " + GameObject.FindGameObjectsWithTag("Node").Count();

        if (holyNode && !extra && !createdHolyNode)
        {
            nodeVC.SetHolyNode(gameObject.name);

            if (Random.Range(0, 5) > 1)
            {
                SpawnNodes(true);
            }
            createdHolyNode = true;
        }
        if (holyNode && extra)
        {
            nodeVC.weight = 100;
        }
    }

    private Vector3 GetCollisionPoint(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 10, forceMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private void ForceToSurface()
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
        if (hitPoint != Vector3.zero) transform.position = hitPoint;
    }

    private void RecalculateRoots()
    {
        colliders = Physics.OverlapSphere(transform.position, maxDistance, layerMask);

        float dist1 = float.MaxValue;
        float dist2 = float.MaxValue;
        float dist3 = float.MaxValue;
        Collider col1 = null;
        Collider col2 = null;
        Collider col3 = null;
        foreach (Collider col in colliders)
        {
            // if same object or player, ignore
            if (col.gameObject == gameObject) continue;
            if (col.gameObject.CompareTag("Player")) continue;

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
        int val = Random.Range(0,30);
        if (col1 != null)
        {
            line1.SetPosition(0, transform.position);
            line1.SetPosition(1, col1.gameObject.transform.position);
        }
        if (col2 != null && val > 5)
        {
            line2.SetPosition(0, transform.position);
            line2.SetPosition(1, col2.gameObject.transform.position);
        }
        if (col3 != null && val > 15)
        {
            line3.SetPosition(0, transform.position);
            line3.SetPosition(1, col3.gameObject.transform.position);
        }
    }

    public void SetHolyNode(string caller)
    {
        //print(caller + " created " + gameObject.name);
        holyNode = true;
    }
}
