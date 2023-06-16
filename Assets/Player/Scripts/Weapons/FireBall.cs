using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    PlayerBullet pBullet;
    Animator bAnim;


    float fallingTime;

    private void Awake()
    {
        pBullet = GetComponent<PlayerBullet>();
        bAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        fallingTime = pBullet._maxBulletTime - 0.6f;
    }


    private void Update()
    {
        fallingTime -= Time.deltaTime;

        if(fallingTime <= 0)
        {
            bAnim.SetTrigger("fall");
        }
    }

}
