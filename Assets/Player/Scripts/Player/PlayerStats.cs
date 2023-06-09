using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    GameManager gManager;

    [SerializeField] int key;

    [SerializeField] Movement pMov;
    public float _pLife;
    public bool enemyKilled;
    public bool isDead;
    public float _calcio;

    [SerializeField] GameObject bow;
    [SerializeField] GameObject fireWand;

    public bool wpnIsBow;
    public bool  winLevel1;
    [SerializeField] LayerMask winLayer;
    public bool canShoot;
    public bool canChangeWpn;
    public int fireBallsCount;

    public float fireWandCooldown;
    public float maxFireWandCooldown;

    [SerializeField] GameObject CuraParts;
    [SerializeField] float heal = 30;


    //BatPis
    public int damagePerSecond = 20;

    private float damageInterval = 1f;
    private float nextDamageTime;

    private void Awake()
    {
    }

    private void Start()
    {
        heal = 30;
        pMov = GetComponent<Movement>();
        _pLife = 100f;
        _calcio = 100f;
        enemyKilled = false;
        wpnIsBow = true;
        canShoot = true;

        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    

    void Update()
    {
        winLevel1 = Physics2D.OverlapCircle(transform.position, 1.5f, winLayer);

        if (!gManager.isPaused)
        {
            //El calcio baja
            _calcio -= Time.deltaTime;

            //Limites del calcio
            _calcio = Mathf.Clamp(_calcio, 0, 100f);

            if (enemyKilled)
            {
                _calcio += 20;
                enemyKilled = false;
            }


            if (Input.GetKeyUp(KeyCode.Q) && canChangeWpn)
            {
                if (!wpnIsBow)
                    wpnIsBow = true;
                else
                    wpnIsBow = false;
            }

            if (wpnIsBow)
            {
                bow.SetActive(true);
                fireWand.SetActive(false);
            }
            else
            {
                bow.SetActive(false);
                fireWand.SetActive(true);
            }


            if (fireBallsCount >= 3)
            {
                fireWandCooldown -= Time.deltaTime;

                if (fireWandCooldown <= 0)
                {
                    fireWandCooldown = maxFireWandCooldown;
                    fireBallsCount = 0;
                    canShoot = true;
                }
            }

        }

        if(_pLife<=0)
        {
            Die();
        }

        if (key >= 1 && winLevel1 && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Level2F");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            key++;
            Destroy(collision.gameObject);
            
        }

        if (collision.CompareTag("Chicken"))
        {
            _pLife += heal;

            if (_pLife >= 100)
            {
                _pLife = 100;
            }

            Destroy(collision.gameObject);
            if (CuraParts != null)
            {
                Instantiate(CuraParts, transform.position, Quaternion.identity, this.transform);
                
            }

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("BatPis"))
        {
            if (Time.time >= nextDamageTime)
            {
                _pLife -= damagePerSecond;
                nextDamageTime = Time.time + damageInterval;

            }

            if (_pLife <= 0)
            {
                Die();
            }

        }
    }

    public void TakeDamage(float damage)
    {
        _pLife += damage;
        
        
        // Muerto? xd
        if (_pLife <= 0f)
        {
            Die();
        }

    }
    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Muerte");
        pMov._rb.simulated = false;
        //pMov._coll.enabled = false;
        //Efectivamente murio
        Debug.Log("Muerto");
        isDead = true;
        GameObject.Find("Guns").SetActive(false);
        SceneManager.LoadScene("Derrota");
    }

    

}
