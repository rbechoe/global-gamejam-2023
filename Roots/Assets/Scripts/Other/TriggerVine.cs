using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVine : MonoBehaviour
{
    public VineAI vineAI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vineAI.StartSpawning();
            Destroy(gameObject);
        }
    }
}
