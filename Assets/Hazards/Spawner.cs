using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] float _sLife;
    [SerializeField] Animator _anim;

    [SerializeField] bool _canSpawn=true;

    [SerializeField] GameObject goblins;
    float timer;
    [SerializeField] float timermax;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _sLife = 300f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0 && _canSpawn)
        {
            GameObject Goblins = Instantiate(goblins, transform.position, Quaternion.identity);
            timer = timermax;
        }
        else if(_sLife <= 0) 
        {
            _canSpawn = false;
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


    void ChangeLife(float value)
    {
        _sLife += value;

    }

}
