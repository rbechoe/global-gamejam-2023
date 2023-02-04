using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materialoffset : MonoBehaviour
{
    public float scrollspeed;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollspeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
