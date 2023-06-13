using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject player;
    public GameObject lever;
    public GameObject barrotes;
    public Collider2D Door;

    public float leverDistance = 1.5f;

    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= leverDistance && Input.GetKeyDown(KeyCode.E)) 
        {
            lever.transform.eulerAngles = new Vector3(0, 0, 60);
            barrotes.transform.position = new Vector3(-4, -2, 0);
            Destroy(Door);
            Debug.Log("Door Unlocked");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, leverDistance);
    }
}