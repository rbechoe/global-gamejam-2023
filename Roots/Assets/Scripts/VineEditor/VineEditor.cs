using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VineEditor : MonoBehaviour
{
    public bool placeMode = false;
    public GameObject collisionPointer;
    public GameObject smartNode;

    private Material pointerMat;

    public Color defaultCol, invalidColor;

    public bool top, bottom, left, right, front, back;
    public Transform topPos, bottomPos, leftPos, rightPos, frontPos, backPos;

    private float spawnCdReset = 0.1f;
    private float spawnCooldown = 0;

    void Start()
    {
        invalidColor = Color.red;
        pointerMat = collisionPointer.GetComponent<MeshRenderer>().material;
        defaultCol = pointerMat.color;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            collisionPointer.transform.position = hit.point;
        }

        int boolCount = 0;
        if (top) boolCount++;
        if (bottom) boolCount++;
        if (left) boolCount++;
        if (right) boolCount++;
        if (front) boolCount++;
        if (back) boolCount++;

        if (boolCount == 5 && placeMode)
        {
            pointerMat.color = defaultCol;
        }
        else
        {
            pointerMat.color = invalidColor;
        }

        if (spawnCooldown >= 0) spawnCooldown -= Time.deltaTime;

        // only 1 should not be touching which implies where it should spawn for no clipping
        if (placeMode && Input.GetMouseButton(0) && boolCount == 5)
        {
            Vector3 spawnPos = Vector3.zero;

            if (!top) spawnPos = topPos.position;
            if (!bottom) spawnPos = bottomPos.position;
            if (!left) spawnPos = leftPos.position;
            if (!right) spawnPos = rightPos.position;
            if (!front) spawnPos = frontPos.position;
            if (!back) spawnPos = backPos.position;

            if (spawnCooldown <= 0)
            {
                GameObject node = Instantiate(smartNode, spawnPos, Quaternion.identity);
                node.name = "Node " + GameObject.FindGameObjectsWithTag("Node").Count();
                spawnCooldown = spawnCdReset;
            }
        }
    }

    public void UpdateBool(bool val, string name)
    {
        switch (name)
        {
            case "top":
                top = val;
                break;
            case "bottom":
                bottom = val;
                break;
            case "left":
                left = val;
                break;
            case "right":
                right = val;
                break;
            case "front":
                front = val;
                break;
            case "back":
                back = val;
                break;
        }
    }
}
