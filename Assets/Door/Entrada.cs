using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrada : MonoBehaviour
{
    public Transform target; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = target.position;
    }
}
