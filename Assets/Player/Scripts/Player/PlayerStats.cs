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

    private void Awake()
    {
    }

    private void Start()
    {
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
        winLevel1 = Physics2D.OverlapCircle(transform.position, 2f, winLayer);

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

        if (key >= 1 && winLevel1 && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(5);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            key++;
            Destroy(collision.gameObject);
            
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

}
