using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    [SerializeField] float lifeSpan;
    [SerializeField] float damagePerSec;

    float timer;

    Animator anim;

    FireBall fBall;
    PlayerBullet pBullet;

    [SerializeField] bool InDamageArea;
    [SerializeField] float radius;
    [SerializeField] LayerMask enemies;
 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        fBall = transform.parent.GetComponent<FireBall>();
        pBullet = transform.parent.GetComponent<PlayerBullet>();
        FindObjectOfType<AudioManager>().Play("Fuego");
    }

    void Update()
    {
        
        

        if (transform.parent != null && transform.parent.childCount > 1)
        {
            Destroy(gameObject);
        }

        //if (transform.parent != null && pBullet._timerBulletTime >= pBullet._maxBulletTime - 0.05f)
        //  transform.parent = null;

        lifeSpan -= Time.deltaTime;

        if (lifeSpan <= 0.3f)
        {
            anim.SetTrigger("zoneDead");
        }

        if (lifeSpan <= 0)
            Destroy(gameObject);

        timer -= Time.deltaTime;
        if (timer <= 0)
            timer = 1.2f;
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(timer >= 1)
                collision.GetComponent<EnemiesLife>().life -= damagePerSec;
        }
        if (collision.CompareTag("Player"))
        {
            if (timer >= 1)
                collision.GetComponent<PlayerStats>()._pLife -= damagePerSec;
        }
    }


}

