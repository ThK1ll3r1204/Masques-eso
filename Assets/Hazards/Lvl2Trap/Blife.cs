using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blife : MonoBehaviour
{
    void Update()
    {
        Destruction();
    }

    public void Destruction()
    {
        Destroy(gameObject, 20f);
    }
}