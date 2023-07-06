using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bDamage;
    public float _maxBulletTime;
    float _timerBulletTime;

    public bool collision;


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
            if (transform.GetComponent<FireBall>() != null && transform.childCount != 0)
                transform.GetChild(0).transform.parent = null;
            Destroy(gameObject);
        }
        else
            collision = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Camera") && !collision.CompareTag("Player") && collision.GetComponent<FireZone>() == null)    
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            StartCoroutine(BulletCollisionDead());
        }
    }


    IEnumerator BulletCollisionDead()
    {
        collision = true;
        if (bAnim != null)
            bAnim.SetTrigger("collision");
        yield return new WaitForSeconds(0.25f);
        if (transform.GetComponent<FireBall>() != null && transform.childCount != 0)
            transform.GetChild(0).transform.parent = null;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
