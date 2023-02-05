using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float rotSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(rotSpeed * Time.deltaTime, rotSpeed/2 * Time.deltaTime, rotSpeed*1.6f * Time.deltaTime);
    }
}
