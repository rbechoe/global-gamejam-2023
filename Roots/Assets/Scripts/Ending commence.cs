using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endingcommence : MonoBehaviour
{
    public GameObject camPlayer, camFinal, audioObjOld, audioObjNew, PP;
    bool trig;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !trig)
        {
            StartCoroutine(StartEnding());
            trig = true;
        }
    }

    public IEnumerator StartEnding()
    {
        Debug.Log("Started timing");

        yield return new WaitForSeconds(2f);
        camPlayer.SetActive(false);
        camFinal.SetActive(true);
        audioObjOld.SetActive(false);
        PP.SetActive(false);
        audioObjNew.SetActive(true);
    }
}
