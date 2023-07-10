using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    GameManager gManager;

    AnimatorClipInfo[] actualAnimation;

    PlayerWpnAiming pAim;
    Animator anim;
    SpriteRenderer pSprite;
    Transform pTransform;
    Movement pMove;
    PlayerStats pStats;

    public bool blockSensation= false;

    private void Awake()
    {
        blockSensation = false;
        anim = this.GetComponent<Animator>();
        pSprite = this.GetComponent<SpriteRenderer>();
        pTransform = this.GetComponent<Transform>();
        pMove = this.GetComponent<Movement>();
        pStats = this.GetComponent<PlayerStats>();

        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        actualAnimation = anim.GetCurrentAnimatorClipInfo(0);
        string aAnimName = actualAnimation[0].clip.name;

        if (!gManager.isPaused)
        {
            pAim = GameObject.Find("Guns").GetComponentInChildren<PlayerWpnAiming>();
            SideIsLooking();

            anim.SetFloat("x", pAim.AimCordFromPlayer.x);
            anim.SetFloat("y", pAim.AimCordFromPlayer.y);


            if (pMove.isWalking)
                anim.SetBool("isWalking", true);
            else
                anim.SetBool("isWalking", false);

            if (pStats.isDead)
            {
                anim.SetTrigger("isDead");
            }

            if (blockSensation && Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("OnBlock");
                
            }
            else if(!blockSensation && Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("OffBlock");
            }

        }
    }



    //Detecta si el jugador apunta a la derecha o izquierda cuando usa el arco
    private void SideIsLooking()
    {
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
    }
}
