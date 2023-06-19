using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnims : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer pSprite;
    [SerializeField] Transform pTrans;
    [SerializeField] EnemiesLife eLife;
    [SerializeField] PatroleMovement GMov;
    [SerializeField] GoblinAttack gAttack;
    [SerializeField] Rigidbody2D gRgbd;
    [SerializeField] Vector3 SeeCordFromPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        pSprite = GetComponent<SpriteRenderer>();
        eLife = GetComponent<EnemiesLife>();
        GMov = GetComponent<PatroleMovement>();
        gAttack = GetComponent<GoblinAttack>();
        gRgbd = GetComponent<Rigidbody2D>();
        pTrans = GameObject.Find("Player").GetComponent<Transform>();
    }
     

    void Update()
    {

        Vector3 pPosFromGobiln = pTrans.position;
        pPosFromGobiln.z = 0f;

        SeeCordFromPlayer = pTrans.localPosition - transform.position;

        
        anim.SetFloat("x", SeeCordFromPlayer.x);
        anim.SetFloat("y", SeeCordFromPlayer.y);

        //Detecta si el jugador apunta a la derecha o izquierda
        if (SeeCordFromPlayer.x < 0f)
        {
            //pTransform.localScale =  Vector3.one + Vector3.left*2;
            pSprite.flipX = true;
        }
        else
        {
            //pTransform.localScale = Vector3.one;
            pSprite.flipX = false;
        }

        if (gAttack.isChasing)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        if (gAttack.isAttacking)
            anim.SetBool("isAttacking", true);
        else
            anim.SetBool("isAttacking", false);


        if (eLife.eDie)
            anim.SetTrigger("isDead");
        
    }
}
