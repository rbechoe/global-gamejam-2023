using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    public GameObject door;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("opened door");

            door.SetActive(false);
            this.gameObject.SetActive(false);
            //handle.transform.rotation.z += 90;
        }
    }
}
