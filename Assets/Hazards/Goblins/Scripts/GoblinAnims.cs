using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnims : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer pSprite;
    [SerializeField] Transform pTrans;
    [SerializeField] EnemiesLife eLife;
    [SerializeField] PatroleMovement gMov;
    [SerializeField] GoblinAttack gAttack;
    [SerializeField] Rigidbody2D gRgbd;
    [SerializeField] Vector3 SeeCordFromPlayer;
    GoblinDetect goblinDetect;


    void Start()
    {
        anim = GetComponent<Animator>();
        pSprite = GetComponent<SpriteRenderer>();
        eLife = GetComponent<EnemiesLife>();
        gMov = GetComponent<PatroleMovement>();
        gAttack = GetComponent<GoblinAttack>();
        gRgbd = GetComponent<Rigidbody2D>();
        pTrans = GameObject.Find("Player").GetComponent<Transform>();
        goblinDetect = GetComponent<GoblinDetect>();
    }
     

    void Update()
    {

        Vector3 pPosFromGobiln = pTrans.position;
        pPosFromGobiln.z = 0f;

        SeeCordFromPlayer = pTrans.localPosition - transform.position;
        


        // Persiguiendo o Caminando
        if (gAttack.isChasing || gMov.isWalking)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        // Ataque
        if (gAttack.isAttacking)
            anim.SetBool("isAttacking", true);
        else
            anim.SetBool("isAttacking", false);

        // Muerte
        if (eLife.eDie)
            anim.SetTrigger("isDead");


        // Patrole Movement
        switch (gMov.sideToLook)
        {
            case -2:
                animDirection(-10, 0);
                pSprite.flipX = true;
                break;
            case 2:
                animDirection(10, 0);
                pSprite.flipX = false;
                break;
            case 1:
                animDirection(0, 10);
                break;
            case -1:
                animDirection(0, -10);
                break;

        }

        // Si el jugador es detectado, seguirlo
        if (goblinDetect.playerFocused)
        {

            //Detecta si el jugador se encuentra a la derecha o izquierda
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

            // Animacion hacia donde este el jugador
            animDirection(SeeCordFromPlayer.x, SeeCordFromPlayer.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            anim.SetTrigger("damage");
        }
    }

    private void animDirection(float directionX, float directionY)
    {
        anim.SetFloat("x", directionX);
        anim.SetFloat("y", directionY);
    }


}

