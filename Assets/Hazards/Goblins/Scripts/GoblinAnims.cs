using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnims : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer pSprite;
    [SerializeField] GameObject pTrans;
    [SerializeField] Vector3 SeeCordFromPlayer;

    void Start()
    {
        
    }

    //Vector3 pPosFromGoblin = ;
    //mPosFromPlayer.z = 0f;

    //    AimCordFromPlayer = mPosFromPlayer - transform.position;

    void Update()
    {
        /*anim.SetFloat("x", pAim.AimCordFromPlayer.x);
        anim.SetFloat("y", pAim.AimCordFromPlayer.y);

        //Detecta si el jugador apunta a la derecha o izquierda
        if (pAim.AimCordFromPlayer.x < 0f)
        {
            //pTransform.localScale =  Vector3.one + Vector3.left*2;
            pSprite.flipX = true;
        }
        else
        {
            //pTransform.localScale = Vector3.one;
            pSprite.flipX = false;
        }

        if (pMove.isWalking)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        if (pStats.isDead)
            anim.SetTrigger("isDead");
        */
    }
}

