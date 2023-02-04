using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeballfollow : MonoBehaviour
{
    private GameObject player;
    public float upvec;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);
    }
    /*
    private void FixedUpdate()
    {

        RaycastHit hit;
        
        
        if (Physics.Raycast(transform.position + Vector3.up * upvec, (player.transform.position - transform.position), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position + Vector3.up * upvec, (player.transform.position - transform.position) * hit.distance, Color.green);
            Debug.Log("Did Hit");

            if(hit.transform.tag == "Player")
            {
                Debug.Log("hit player");
            }
        }
        else
        {
            Debug.DrawRay(transform.position, (player.transform.position - transform.position) * hit.distance, Color.red);
            Debug.Log("Did not Hit");
        }
    }*/
}
