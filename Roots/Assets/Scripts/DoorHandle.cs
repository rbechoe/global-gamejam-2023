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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            Debug.Log("hit handel box");

            //show text

            //teleport player

        }
    }
}
