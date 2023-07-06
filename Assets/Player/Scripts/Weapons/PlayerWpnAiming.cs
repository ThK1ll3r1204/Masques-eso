using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWpnAiming : MonoBehaviour
{
    //Fabri

    public GameObject thisGun;
    public GameObject pCorpse;
    public PlayerStats pStats;

    SpriteRenderer wSprite;
    Animator wAnimator;
    Animator pAnimator;

    AnimatorClipInfo[] actualAnimation;

    [SerializeField] GameObject _pBullet;
    [SerializeField] float _bSpeed;
    [SerializeField] float _bCooldown;
    public Vector3 AimCordFromPlayer;
    public Vector3 mPosFromPlayer;

    bool AimingRight;


    [SerializeField] Transform _firePoint;
    private float _bCooldowntimer;



    void Awake()
    {
        pCorpse = GameObject.Find("Player");
        pStats = pCorpse.GetComponent<PlayerStats>();
        pAnimator = pCorpse.GetComponent<Animator>();
        thisGun = this.gameObject;
        wSprite = this.GetComponent<SpriteRenderer>();
        wAnimator = this.GetComponent<Animator>();
    }

    private void Start()
    {
    }

    void Update()
    {
        Vector3 mPosFromPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosFromPlayer.z = 0f;

        AimCordFromPlayer = mPosFromPlayer - pCorpse.transform.position;

        actualAnimation = pAnimator.GetCurrentAnimatorClipInfo(0);
        string aAnimName = actualAnimation[0].clip.name;


        if (pStats.wpnIsBow)
        {
            // Rotacion del arco
            float angle = Mathf.Atan2(AimCordFromPlayer.y, AimCordFromPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Eje al que apunta el arco
            BowAimingOnAxis(aAnimName);

            // Posicion dependiendo a donde apunta el arco
            BowAimingPosition(aAnimName);

            pStats.canShoot = true;
        }
        else
        {
            transform.rotation = Quaternion.identity;
            // Eje al que mira el baculo
            FireWandAimingOnAxis();

            // Posicion dependiendo a donde apunta el jugador
            FireWandAimingPosition(aAnimName);
        }

        if (pStats.fireBallsCount >= 3 && !pStats.wpnIsBow)
        {
            pStats.canShoot = false;
            wAnimator.SetBool("Cooldown", true);
        }


        if (pStats.fireBallsCount <= 0 && !pStats.wpnIsBow)
            wAnimator.SetBool("Cooldown", false);


        //Disparo
        if (Input.GetMouseButton(0) && _bCooldowntimer <= 0 && pStats.canShoot)
        {
            wAnimator.SetTrigger("Shoot");
                float angle = Mathf.Atan2(AimCordFromPlayer.y, AimCordFromPlayer.x) * Mathf.Rad2Deg;
                GameObject bullet = Instantiate(_pBullet, _firePoint.position, Quaternion.identity);
                bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bullet.GetComponent<Rigidbody2D>().velocity = AimCordFromPlayer.normalized * _bSpeed;
                _bCooldowntimer = _bCooldown;
            FindObjectOfType<AudioManager>().Play("Disparo");

            if(!pStats.wpnIsBow)
            {
                pStats.fireBallsCount += 1;
            }

            //if(pStats.wpnIsBow)
            //FindObjectOfType<AudioManager>().Play("Disparo");
        }
    }

    private void FireWandAimingOnAxis()
    {
        //Detecta si el jugador apunta a la derecha o izquierda
        if (AimCordFromPlayer.x > 0f)
        {
            AimingRight = true;
        }
        else
        {
            AimingRight = false;
        }
    }
    private void FireWandAimingPosition(string aAnimName)
    {
        switch (aAnimName)
        {
            // Apunta a arriba y al lateral derecho
            case "Walk_Lat" when AimingRight:
            case "Idle_Lat" when AimingRight:
                wSprite.flipX = false;
                wSprite.sortingOrder = 1;
                thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.114f + Vector3.right * 0.437f;
                break;
            // Apunta a arriba y al lateral izquierdo
            case "Walk_Lat" when !AimingRight:
            case "Idle_Lat" when !AimingRight:
                wSprite.flipX = true;
                wSprite.sortingOrder = 3;
                thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.061f + Vector3.right * -0.538f;
                break;
            //  Apunta abajo
            case "Walk":
            case "Idle":
                wSprite.flipX = false;
                wSprite.sortingOrder = 3;
                thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.035f + Vector3.right * 0.592f;
                break;
            // Apunta en diagonal abajo
            case "Idle_Diag_D":
            case "Walk_Diag_D":
                if (AimingRight) // Derecha
                {
                    wSprite.flipX = false;
                    wSprite.sortingOrder = 1;
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.149f + Vector3.right * 0.489f;
                }
                else // Izquierda
                {
                    wSprite.flipX = true;
                    wSprite.sortingOrder = 3;
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.107f + Vector3.right * 0.18f; 
                }
                break;
            // Apunta en diagonal arriba
            case "Idle_Diag_U":
            case "Walk_Diag_U":
                if (AimingRight) // Derecha
                {
                    wSprite.flipX = false;
                    wSprite.sortingOrder = 1;
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.227f + Vector3.right * 0.39f;
                }
                else // Izquierda
                {
                    wSprite.flipX = true;
                    wSprite.sortingOrder = 3;
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.041f + Vector3.right * -0.477f; 
                }
                break;
            // Apunta arriba
            case "Idle_U":
            case "Walk_U":
                wSprite.sortingOrder = 1;
                wSprite.flipX = true;
                thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.341f + Vector3.right * -0.598f;
                break;
        }

    }

    private void BowAimingOnAxis(string aAnimName)
    {
        //Detecta si el jugador apunta a la derecha o izquierda
        if (AimCordFromPlayer.x > 0f)
        {
            AimingRight = true;
            //thisGun.transform.position = pCorpse.transform.position + Vector3.right * 0.522f;
            wSprite.flipY = false;
        }
        else
        {
            AimingRight = false;
            //thisGun.transform.position = pCorpse.transform.position + Vector3.left * 0.522f;
            wSprite.flipY = true;
        }

        bool lookinLat;

        if (aAnimName == "Walk_Lat" || aAnimName == "Idle_Lat")
            lookinLat = true;
        else
            lookinLat = false;


        // El jugador apunta abajo
        if (AimCordFromPlayer.y < 0f && !lookinLat)
        {
                wSprite.sortingOrder = 2;
        }
        // El jugador apunta arriba
        else if (lookinLat || AimCordFromPlayer.y > 0)
        {
            wSprite.sortingOrder = 1;
        }
    }
    private void BowAimingPosition(string aAnimName)
    {
        switch (aAnimName)
        {
            // Apunta a arriba y al lateral derecho
            case "Walk_Lat" when /*!AimingUp &&*/ AimingRight:
            case "Idle_Lat" when /*!AimingUp &&*/ AimingRight:
                thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.166f + Vector3.right * 0.289f;
                break;
            // Apunta a arriba y al lateral izquierdo
            case "Walk_Lat" when /*!AimingUp &&*/ !AimingRight:
            case "Idle_Lat" when /*!AimingUp &&*/ !AimingRight:
                thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.059f + Vector3.right * -0.289f;
                break;
            /* Apunta a abajo y al lateral derecho
            case "Walk_Lat" when AimingUp && AimingRight:
            case "Idle_Lat" when AimingUp && AimingRight:
                thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.127f + Vector3.right * 0.318f;
                break;
            // Apunta a abajo y al lateral izquierdo
            case "Walk_Lat" when AimingUp && !AimingRight:
            case "Idle_Lat" when AimingUp && !AimingRight:
                thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.127f + Vector3.right * -0.318f;
                break; */
            //  Apunta abajo
            case "Walk":
            case "Idle":
                if (AimingRight) // Derecha
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.6f + Vector3.right * -0.201f;
                else  // Izquierda
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.6f + Vector3.right * 0.201f;
                break;
            // Apunta en diagonal abajo
            case "Idle_Diag_D":
            case "Walk_Diag_D":
                if (AimingRight) // Derecha
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.515f + Vector3.right * 0.276f;
                else // Izquierda
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * -0.515f + Vector3.right * -0.276f;
                break;
            // Apunta en diagonal arriba
            case "Idle_Diag_U":
            case "Walk_Diag_U":
                if (AimingRight) // Derecha
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.227f + Vector3.right * 0.39f;
                else // Izquierda
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.227f + Vector3.right * -0.39f;
                break;
            // Apunta arriba
            case "Idle_U":
            case "Walk_U":
                if (AimingRight) // Derecha
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.45f + Vector3.right * -0.08f;
                else // Izquierda
                    thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.45f + Vector3.right * 0.08f;
                break;
        }
    }

    private void FixedUpdate()
    {
        //Cooldown del disparo
        if (_bCooldowntimer > 0f)
        {
            _bCooldowntimer -= Time.deltaTime;
        }

    }

}
