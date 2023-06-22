using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWpnAimingB : MonoBehaviour
{
    //Fabri

    public GameObject thisGun;
    public GameObject pCorpse;
    public PlayerStats pStats;

    SpriteRenderer wSprite;
    Animator wAnimator;
    Animator pAnimator;

    AnimatorClipInfo[] actualAnimation;

    
    [SerializeField] float _bCooldown;
    public Vector3 AimCordFromPlayer;
    public Vector3 mPosFromPlayer;

    bool AimingRight;
    bool AimingUp;

    [SerializeField] Transform _firePoint;
    private float _bCooldowntimer;

    //Báculo
    public GameObject bulletPrefab;  // Prefab de la bala
    public float bulletSpeed = 10f;  // Velocidad de la bala
    public float explosionRadius = 2f;  // Radio del área de daño
    public float explosionDuration = 3f;  // Duración del área de daño
    public float damageInterval = 1f;  // Intervalo de daño al enemigo


    void Awake()
    {
        pCorpse = GameObject.Find("Player");
        pStats = pCorpse.GetComponent<PlayerStats>();
        pAnimator = pCorpse.GetComponent<Animator>();
        thisGun = this.gameObject;
        wSprite = this.GetComponent<SpriteRenderer>();
        wAnimator = this.GetComponent<Animator>();
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
                       
        }
        else
        {
            transform.rotation = Quaternion.identity;
            // Eje al que mira el baculo
            FireWandAimingOnAxis();

            // Posicion dependiendo a donde apunta el jugador
            FireWandAimingPosition(aAnimName);
        }


        //Disparo
        if (Input.GetMouseButton(0) && _bCooldowntimer <= 0)
        {
            wAnimator.SetTrigger("Shoot");

            

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            // Disparar la bala en la dirección calculada
            bulletRigidbody.velocity = AimCordFromPlayer.normalized * bulletSpeed;

            //Controlar cadencia de disparo
            _bCooldowntimer = _bCooldown;

            // Asignar el script de explosión a la bala
            Explosion explosionScript = bullet.GetComponent<Explosion>();
            explosionScript.Initialize(explosionRadius, explosionDuration, damageInterval);
            _bCooldowntimer = _bCooldown;
            


            
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

    

    private void FixedUpdate()
    {
        //Cooldown del disparo
        if (_bCooldowntimer > 0f)
        {
            _bCooldowntimer -= Time.deltaTime;
        }

    }

}
