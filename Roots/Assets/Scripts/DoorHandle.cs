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
        charCon = player.GetComponent<CharacterController>();
        plaCon = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hit handel box");

            charCon.enabled = false;
            plaCon.enabled = false;

            player.transform.position = coupe2;

            charCon.enabled = true;
            plaCon.enabled = true;

            gameObject.SetActive(false);
        }
    }
}
