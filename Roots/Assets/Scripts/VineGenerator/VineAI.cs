using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VineAI : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;

    public GameObject AINode;

    private float startWeight;

    void Start()
    {
        startWeight = 100;
        transform.position = startPoint.transform.position;

        SpawnNodes(3, true);
    }

    void SpawnNodes(int maxSize, bool forceMax = false)
    {
        int minSize = (forceMax) ? maxSize : 1;
        int amount = Random.Range(minSize, maxSize);

        for(int i = 0; i < amount; i++)
        {
            GameObject newNode = Instantiate(AINode, transform.position, Quaternion.identity);
            newNode.transform.eulerAngles += new Vector3(Random.Range(0, 30) - 15, Random.Range(0, 30) - 15, Random.Range(0, 30) - 15);
            VineConnector nodeVC = newNode.GetComponent<VineConnector>();
            nodeVC.weight = startWeight;
            nodeVC.endPoint = endPoint;
            nodeVC.parentNode = gameObject;
            if (i == 0) nodeVC.SetHolyNode(gameObject.name);
            newNode.name = "Node " + GameObject.FindGameObjectsWithTag("Node").Count();
        }
    }
}
