using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float bDamage;
    [SerializeField] float _maxBulletTime;
    private float _timerBulletTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _timerBulletTime += Time.deltaTime;

        if (_timerBulletTime >= _maxBulletTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
           Destroy(gameObject);
    }
}
