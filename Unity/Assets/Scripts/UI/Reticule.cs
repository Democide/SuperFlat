using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{

    Vector3 mousePos;

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
