using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLife : MonoBehaviour
{
    public float life;
    public PlayerBullet pBullet;
    public PlayerStats pStats;
    public bool eDie = false;
    public GameObject PEffect;


    private void Awake()
    {
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
           
            pBullet = collision.GetComponent<PlayerBullet>();
            float nextDamage = pBullet.bDamage;
            BeingDamaged(nextDamage);
        }
    }

    private void Update()
    {
        if (life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        pStats.enemyKilled = true;
        eDie = true;
        Destroy(this.gameObject);
        if (PEffect != null)
        {
            Instantiate(PEffect, transform.position, Quaternion.identity);
        }
    }

    public void BeingDamaged(float damage)
    {
        life -= damage;
        
    }

}
