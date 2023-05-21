using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWpnAiming : MonoBehaviour
{
    public GameObject thisGun;
    public GameObject pCorpse;

    SpriteRenderer wSprite;
    Animator wAnimator;
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
        thisGun = this.gameObject;
        wSprite = this.GetComponent<SpriteRenderer>();
        wAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 mPosFromPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosFromPlayer.z = 0f;

        AimCordFromPlayer = mPosFromPlayer - transform.position;
        float angle = Mathf.Atan2(AimCordFromPlayer.y, AimCordFromPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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

        if (AimCordFromPlayer.y < 0f)
        {
            if (AimingRight)
            thisGun.transform.position = pCorpse.transform.position + Vector3.down * 0.114f + Vector3.left * 0.522f;
            else
            thisGun.transform.position = pCorpse.transform.position + Vector3.down * 0.114f + Vector3.right * 0.522f;
            wSprite.sortingOrder = 3;
        }
        else
        {
            if(AimingRight)
            thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.114f + Vector3.left * 0.522f; 
            else
            thisGun.transform.position = pCorpse.transform.position + Vector3.up * 0.114f + Vector3.right * 0.522f;
            wSprite.sortingOrder = 1;
        }



        //Disparo
        if (Input.GetMouseButton(0) && _bCooldowntimer <= 0)
        {
            GameObject bullet = Instantiate(_pBullet, _firePoint.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.GetComponent<Rigidbody2D>().velocity = AimCordFromPlayer.normalized * _bSpeed;
            _bCooldowntimer = _bCooldown;
            wAnimator.SetTrigger("Shoot");
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWpnAiming : MonoBehaviour
{
    [SerializeField] GameObject thisGun;
    [SerializeField] GameObject pCorpse;

    [SerializeField] GameObject _pBullet;
    [SerializeField] float _bSpeed;
    [SerializeField] float _bCooldown;

    [SerializeField] Transform _firePoint;
    private float _bCooldowntimer;
    void Awake()
    {
        pCorpse = GameObject.Find("Player");
        thisGun = this.gameObject;
    }

    void Update()
    {
        Vector3 mPosFromPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosFromPlayer.z = 0f;

        Vector3 direction = mPosFromPlayer - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Detecta si el jugador apunta a la derecha o izquierda
        if (mPosFromPlayer.x < pCorpse.transform.position.x)
        {
            thisGun.transform.position = pCorpse.transform.position + Vector3.left * 0.504f;
        }
        else
        {
            thisGun.transform.position = pCorpse.transform.position + Vector3.right * 0.504f;
        }

        //Disparo
        if (Input.GetMouseButton(0) && _bCooldowntimer <= 0)
        {
            GameObject bullet = Instantiate(_pBullet, _firePoint.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * _bSpeed;
            _bCooldowntimer = _bCooldown;
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
