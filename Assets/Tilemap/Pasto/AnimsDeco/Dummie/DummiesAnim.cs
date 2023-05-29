using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummiesAnim : MonoBehaviour
{
    [SerializeField] Animator _anim;

    float life =100f;
    bool ChangeLife = false;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            life--;
            ChangeLife = true;
            if (ChangeLife)
            {
                _anim.SetBool("damage", true);
            }
           
            else 
            {
                ChangeLife = false;
                _anim.SetBool("damage", false);
            }
            
        }
        
       
    }


}
