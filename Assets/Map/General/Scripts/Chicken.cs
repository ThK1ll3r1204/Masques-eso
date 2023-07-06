using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    PlayerStats pStats;
    [SerializeField] float heal;
    void Awake()
    {
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pStats._pLife += heal;

            Destroy(gameObject);
        }
    }

}
