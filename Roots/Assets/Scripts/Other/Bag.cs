using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public StartGame game;
    public GameObject hitter;
    public AudioSource source;
    public GameObject[] enables;
    public GameObject[] transObjs;
    public LayerMask layerMask;
    public GameObject backgroundObj;
    private Material backgroundMat;
    public GameObject interact;
    private Material myMat;

    bool selected;

    float distance = 0;

    private Color selectedCol, normalCol;

    void Start()
    {
        myMat = GetComponent<Renderer>().material;
        selectedCol = Color.green;
        normalCol = myMat.color;

        backgroundMat = backgroundObj.GetComponent<Renderer>().material;
        foreach (GameObject obj in enables)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            hitter.transform.position = hit.point;
        }

        distance = Vector3.Distance(transform.position, hitter.transform.position);

        if (selected)
        {
            interact.SetActive(true);
            myMat.color = selectedCol;

            if (Input.GetKeyDown(KeyCode.E))
            {
                source.volume = 1;
                game.StartTrip();
                foreach(GameObject obj in enables)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject t in transObjs)
                {
                    t.SetActive(false);
                }
                interact.SetActive(false);
                myMat.color = normalCol;
                Destroy(this);
            }
        }
        else
        {
            interact.SetActive(false);
            myMat.color = normalCol;
        }
    }

    private void FixedUpdate()
    {
        // TODO the closer it gets the less transparent the environment
        source.volume = Mathf.Clamp(.3f - distance / 5f, 0.05f, 0.3f);
        backgroundMat.color = new Color(1, 1, 1, Mathf.Clamp(.3f - distance / 4f, 0, 0.3f));

        foreach(GameObject t in transObjs)
        {
            Material mat = t.GetComponent<Renderer>().material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, Mathf.Clamp(1 - distance / 4f, 0, 1));
        }
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
