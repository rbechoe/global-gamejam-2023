using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    CharacterController charCon;
    PlayerController plaCon;

    public GameObject player;

    public Vector3 coupe2;

    void Start()
    {
        //   player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        charCon.enabled = false;
        plaCon.enabled = false;

            Debug.Log("opened door");

            player.transform.position = coupe2;

        charCon.enabled = true;
        plaCon.enabled = true;
    }
}
