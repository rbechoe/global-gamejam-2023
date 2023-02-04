using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveLamps : MonoBehaviour
{
    public GameObject[] raveLamps;
    public List<Material> lights;

    Color defaultCol;
    Color offCol = Color.black;

    void Start()
    {
        foreach(GameObject lamp in raveLamps)
        {
            lights.Add(lamp.GetComponent<Renderer>().material);
        }

        defaultCol = lights[0].color;

        StartCoroutine(Rave());
    }

    IEnumerator Rave()
    {
        foreach (Material lamp in lights)
        {
            lamp.EnableKeyword("_EMISSION");
            lamp.color = offCol;
            lamp.SetColor("_EmissiveColor", offCol);
        }

        while (true)
        {
            int raveSet = Random.Range(0, 10);
            if (raveSet < 3)
            {
                foreach (Material lamp in lights)
                {
                    lamp.color = defaultCol;
                    lamp.SetColor("_EmissiveColor", defaultCol * 17000);
                    yield return new WaitForSeconds(0.025f);
                    lamp.color = offCol;
                    lamp.SetColor("_EmissiveColor", offCol);
                }
            }
            else if (raveSet < 5)
            {
                for (int i = 0; i < 10; i++)
                {
                    foreach (Material lamp in lights)
                    {
                        lamp.color = defaultCol;
                        lamp.SetColor("_EmissiveColor", defaultCol * 17000);
                    }
                    yield return new WaitForSeconds(0.1f);
                    foreach (Material lamp in lights)
                    {
                        lamp.color = offCol;
                        lamp.SetColor("_EmissiveColor", offCol);
                    }
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                foreach (Material lamp in lights)
                {
                    Color randomCol = new Color(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
                    lamp.color = randomCol;
                    lamp.SetColor("_EmissiveColor", randomCol * 17000);
                    yield return new WaitForSeconds(0.025f);
                    lamp.color = offCol;
                    lamp.SetColor("_EmissiveColor", offCol);
                }
            }
        }

        yield return new WaitForEndOfFrame();
    }
}
