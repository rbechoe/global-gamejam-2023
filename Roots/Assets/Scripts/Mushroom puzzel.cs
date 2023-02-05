using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroompuzzel : MonoBehaviour
{
    public PlayerManager player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            //Debug.Log("hit mushroom");

            PlayerManager.mushroomNum++;

            this.gameObject.SetActive(false);

            //show text

            //teleport player

        }
    }
}
