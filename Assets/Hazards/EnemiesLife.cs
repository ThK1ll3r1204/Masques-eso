using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLife : MonoBehaviour
{
    [SerializeField] public float life;
    [SerializeField] public PlayerBullet pBullet;
    [SerializeField] public PlayerStats pStats;

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
            pStats.enemyKilled = true;
            Destroy(this.gameObject);
        }
    }
    void BeingDamaged(float damage)
    {
        life -= damage;
    }

}
