using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummiesAnim : MonoBehaviour
{
    [SerializeField] Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            //_anim.SetBool("damage",true);
            _anim.Play(2);
        }
        
       
    }

}
