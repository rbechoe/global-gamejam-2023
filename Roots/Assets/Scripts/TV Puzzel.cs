using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVPuzzel : MonoBehaviour
{
    public GameObject[] tvs;
    public bool tv1;
    public bool tv2;
    public bool tv3;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Did Hit" + hit.transform.name);

                if (hit.transform.name == "TV1")
                {
                    tv1 = true;
                }
                if (hit.transform.name == "TV2")
                {
                    tv2 = true;
                }
                if (hit.transform.name == "TV3")
                {
                    tv3 = true;
                }
                if (tv1 && tv2 && tv3)
                {
                    Debug.Log("eerste puzzle gehaald");
                }
            }
        }
        else
        {
            
        }
    }

    
}
