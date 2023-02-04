using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeballfollow : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);
    }
}
