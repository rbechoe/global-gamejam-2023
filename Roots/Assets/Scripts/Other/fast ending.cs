using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fastending : MonoBehaviour
{
    bool selected = false;
    bool enab = false;
    public GameObject ui;
    public GameObject audioEnd;
    public GameObject wallEnd;
    public GameObject[] disables;

    Materialoffset mos;

    void Start()
    {
        mos = wallEnd.GetComponent<Materialoffset>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected && !enab)
        {
            ui.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                enab = true;
                audioEnd.GetComponent<AudioSource>().Play();
                StartCoroutine(StartEnding());

                foreach (GameObject obj in disables)
                {
                    obj.SetActive(false);
                }
            }
        }
        else
        {
            ui.SetActive(false);
        }
    }

    IEnumerator StartEnding()
    {
        for (int i = 0; i < 20; i++)
        {
            mos.scrollspeed += 0.025f;
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);
    }

    private void OnMouseEnter()
    {
        selected = true;
    }

    private void OnMouseExit()
    {
        selected = false;
    }
}
