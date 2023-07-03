using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    PlayerBullet pBullet;
    Animator bAnim;
    [SerializeField] GameObject fireZone;


    float fallingTime;
    public bool fireZoneCreated;

    private void Awake()
    {
        pBullet = GetComponent<PlayerBullet>();
        bAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        fallingTime = pBullet._maxBulletTime - 0.6f;
        fireZoneCreated = false;
    }


    private void Update()
    {
        fallingTime -= Time.deltaTime;

        if (pBullet.collision && this.transform.childCount == 0)
        {
            Instantiate(fireZone, transform.position, Quaternion.identity, this.transform);
            Debug.Log("creao");
        }

        if (fallingTime <= 0)
        {
            bAnim.SetTrigger("fall");
            if (fallingTime <= -0.2f  && this.transform.childCount == 0)
            {
                pBullet.rb.constraints = RigidbodyConstraints2D.FreezePosition;
                Instantiate(fireZone, transform.position, Quaternion.identity, this.transform);          
                Debug.Log("creao");
            }
        }
    }

}
