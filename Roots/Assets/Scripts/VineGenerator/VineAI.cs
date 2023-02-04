using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VineAI : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;

    public GameObject AINode;

    public List<GameObject> lastNodes = new List<GameObject>();

    private Quaternion startRotation;
    private float startWeight;
    private float clampLimit = 5;
    private Transform fakeTransform;

    Vector3 eulerUpdate;

    void Start()
    {
        startWeight = 100;
        startRotation = transform.rotation;

        Vector3 relativePos = endPoint.transform.position - startPoint.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        eulerUpdate = transform.eulerAngles - desiredRotation.eulerAngles;
        eulerUpdate = new Vector3(Mathf.Clamp(eulerUpdate.x, -clampLimit, clampLimit), 
                                  Mathf.Clamp(eulerUpdate.y, -clampLimit, clampLimit), 
                                  Mathf.Clamp(eulerUpdate.z, -clampLimit, clampLimit));

        StartCoroutine(GrowRoots());
    }

    IEnumerator GrowRoots()
    {
        // TODO spawn 3 things and then iterate
        fakeTransform = transform;
        fakeTransform.eulerAngles += eulerUpdate * 2;
        fakeTransform.position += fakeTransform.forward * 2;
        GameObject nodeA = Instantiate(AINode, fakeTransform.position, Quaternion.identity);
        lastNodes.Add(nodeA);

        fakeTransform = transform;
        fakeTransform.eulerAngles -= eulerUpdate * 2;
        fakeTransform.position += fakeTransform.forward * 2;
        GameObject nodeB = Instantiate(AINode, fakeTransform.position, Quaternion.identity);
        lastNodes.Add(nodeB);

        fakeTransform = transform;
        fakeTransform.eulerAngles = eulerUpdate;
        fakeTransform.position += fakeTransform.forward * 2;
        GameObject nodeC = Instantiate(AINode, fakeTransform.position, Quaternion.identity);
        lastNodes.Add(nodeC);

        startRotation.eulerAngles = eulerUpdate;
        // TODO check if new child is within 0.1 distance of end position, otherwise continue spawning every .25 seconds

        yield return new WaitForEndOfFrame();
    }
}
