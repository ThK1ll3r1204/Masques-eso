using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float bDamage;
    public float _maxBulletTime;
    private float _timerBulletTime;


    Animator bAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bAnim = GetComponent<Animator>();
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
        if (!collision.CompareTag("Player"))
        {
            StartCoroutine(BulletCollisionDead());
        }
    }


    IEnumerator BulletCollisionDead()
    {
        if (bAnim != null)
            bAnim.SetTrigger("collision");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
