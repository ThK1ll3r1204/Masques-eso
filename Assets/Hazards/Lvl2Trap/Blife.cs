using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blife : MonoBehaviour
{
    [SerializeField] PlayerStats pStats;

    void Update()
    {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Destruction();
    }

    public void Destruction()
    {
        Destroy(gameObject, 20f);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Player"))
        {
            pStats.TakeDamage(-10);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Limit"))
        {
            Destroy(this.gameObject);
        }
    }
}