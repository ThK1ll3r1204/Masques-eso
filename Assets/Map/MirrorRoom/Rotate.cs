using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 1f;
    public int rotation = -360;


    private void Update()
    {
        transform.Rotate(0, rotation * speed * Time.deltaTime, 0);
    }
}