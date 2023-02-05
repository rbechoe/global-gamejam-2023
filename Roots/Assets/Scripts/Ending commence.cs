using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endingcommence : MonoBehaviour
{
    public GameObject camPlayer, camFinal;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        StartCoroutine(StartEnding());
    }

    public IEnumerator StartEnding()
    {
        Debug.Log("Started timing");

        yield return new WaitForSeconds(5f);
    }
}
