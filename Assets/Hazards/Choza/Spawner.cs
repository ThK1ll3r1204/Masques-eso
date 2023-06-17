using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float _sLife;
    [SerializeField] Animator _anim;

    public bool _canSpawn = true;

    [SerializeField] Transform firePoint;

    [SerializeField] GameObject goblins;
    public float timer;
    [SerializeField] float timermax;

    public bool dead;
    public bool takeDamage;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] float radius;


    void Awake()
    {
        _anim = GetComponent<Animator>();
        dead = false;
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

        if(_sLife <= 150)
        {
            _anim.SetBool("Half", true);
            _anim.SetBool("Destroy", false);
        }

        if(_sLife<=0)
        {
            _anim.SetBool("Destroy", true);
            _anim.SetBool("Half", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            ChangeLife(-30f);
        }
    }


    public void ChangeLife(float value)
    {
        _sLife += value;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
