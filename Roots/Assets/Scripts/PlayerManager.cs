using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static float mushroomNum;

    void Start()
    {
        
    }

    void Update()
    {
        if (mushroomNum == 3)
        {
            Debug.Log("you eat all the mushroom");
        }
    }

    void AddmuschrromNum()
    {
        mushroomNum++;
    }
}
