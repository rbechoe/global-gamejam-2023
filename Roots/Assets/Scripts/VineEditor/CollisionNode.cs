using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNode : MonoBehaviour
{
    public VineEditor vineEditor;

    public bool colliding = false;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        colliding = true;
        vineEditor.UpdateBool(colliding, gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
        vineEditor.UpdateBool(colliding, gameObject.name);
    }
}
