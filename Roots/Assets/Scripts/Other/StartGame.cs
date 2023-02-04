using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject startCam, playCam;
    public PlayerController playerController;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartTrip()
    {
        startCam.SetActive(false);
        playCam.SetActive(true);
        playerController.enabled = true;
    }
}
