using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class VFXTuner : MonoBehaviour
{
    public Volume volume;
    Bloom myBloom;
    ChromaticAberration chrom;
    PaniniProjection paniniProjection;
    float r, g, b;
    float updatecd;

    float panVal = 230;
    float intensity = 1000;
    bool down;

    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out Bloom bloom);
        myBloom = bloom;
        volume.profile.TryGet(out ChromaticAberration ca);
        chrom = ca;
        volume.profile.TryGet(out PaniniProjection pi);
        paniniProjection = pi;
    }

    // Update is called once per frame
    void Update()
    {
        if (updatecd <= 0)
        {
            r = Random.Range(0, 255) / 255f / 100;
            g = Random.Range(0, 255) / 255f / 100;
            b = Random.Range(0, 255) / 255f / 100;
            myBloom.tint.value = new Color(r, g, b, 1);
            myBloom.intensity.value = Random.Range(350, 400) / 1200f;
            updatecd = 0.25f;
        }
        else
        {
            updatecd -= Time.deltaTime;
        }

        if (down)
        {
            intensity -= Time.deltaTime * 600f;
            panVal += Time.deltaTime * 480;
        }
        else
        {
            intensity += Time.deltaTime * 800f;
            panVal -= Time.deltaTime * 640;
        }
        if (intensity < 500)
        {
            down = false;
        }
        if (intensity > 1000)
        {
            down = true;
        }
    }

    private void FixedUpdate()
    {
        myBloom.dirtIntensity.value = Random.Range(425, 500) / 100f;
        chrom.intensity.value = intensity / 1000f;
        paniniProjection.distance.value = panVal / 1000f;
    }
}
