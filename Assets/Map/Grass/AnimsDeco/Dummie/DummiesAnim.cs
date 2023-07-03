using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummiesAnim : MonoBehaviour
{
    //Piero


    public Animator _anim;


    void Start()
    {
        //Pone el animator automaticamente en el inspector
        _anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Detecta collision con PlayerBullet
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            
            // Ejecuta la animación
            _anim.SetTrigger("golpeao");
            Debug.Log("caca");
        }
        
       
    }


}
