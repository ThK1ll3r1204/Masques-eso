using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGAnimations : MonoBehaviour
{
    public float _sLife;
    [SerializeField] Animator _anim;

    public bool _canSpawn = true;

    [SerializeField] Transform firePoint;

    [SerializeField] GameObject goblins;
    public float timer;
    [SerializeField] float timermax;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] float radius;



    void Awake()
    {
        _anim = GetComponent<Animator>();
        _sLife = 300f;
    }

    void Update()
    {
        _canSpawn = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (_canSpawn)
        {
            _canSpawn = true;

            timer -= Time.deltaTime;

            if (timer <= 0 && _canSpawn)
            {
                _anim.SetBool("Spawn",true);
                GameObject Goblins = Instantiate(goblins, firePoint.transform.position, Quaternion.identity);
                timer = timermax;
                FindObjectOfType<AudioManager>().Play("Spawn");
            }
            else if (_sLife <= 0)
            {
                _canSpawn = false;
                radius = 0;
                
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            ChangeLife(-20f);
        }
    }


    public void ChangeLife(float value)
    {
        _sLife += value;
        _anim.SetTrigger("Hurt");

        if (_sLife <= 0)
        {
            _anim.SetBool("IsDead", true);

            Destroy(gameObject, 1f);

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
