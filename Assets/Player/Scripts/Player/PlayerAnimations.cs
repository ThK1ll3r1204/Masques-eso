using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerWpnAiming pAim;
    Animator anim;
    SpriteRenderer pSprite;
    Transform pTransform;
    Movement pMove;
    PlayerStats pStats;

    private void Awake()
    {
        pAim = GameObject.Find("Guns").GetComponentInChildren<PlayerWpnAiming>();
        anim = this.GetComponent<Animator>();
        pSprite = this.GetComponent<SpriteRenderer>();
        pTransform = this.GetComponent<Transform>();
        pMove = this.GetComponent<Movement>();
        pStats = this.GetComponent<PlayerStats>();
    }

    void Update()
    {
        anim.SetFloat("x", pAim.AimCordFromPlayer.x);
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
    }
}