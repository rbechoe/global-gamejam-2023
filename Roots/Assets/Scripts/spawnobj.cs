using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnobj : MonoBehaviour
{
    public GameObject obj;

    private Vector3 center;
    public Vector3 size;

    public float amount;

    void Start()
    {
        center = transform.position;
        SpawnItem();
    }

    void Update()
    {

    }

    void SpawnItem()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -.5f, (Random.Range(-size.z / 2, size.z / 2)));

            Quaternion rot = Quaternion.Euler(Random.Range(0, 360), 0, Random.Range(0, 360));

            Instantiate(obj, pos, rot);
        }
    }
}
