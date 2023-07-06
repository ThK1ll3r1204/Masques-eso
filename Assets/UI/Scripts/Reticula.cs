using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticula : MonoBehaviour
{
    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}

