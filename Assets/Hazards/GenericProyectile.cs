using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericProyectile : MonoBehaviour
{
    [SerializeField] PlayerStats pStats;
    [SerializeField] float damage;
    [SerializeField] Rigidbody2D rb2d;

    [SerializeField] PlayerAnimations anims;

    public GameObject PEffect;

    public GameObject shooter;

    

    //Detector raycast
    [field: SerializeField]
    public bool playerDetected { get; private set; }

    [Header("Circle Detector parameters")]
    [SerializeField]
    public Transform detectorOrigin;
    [Range(0.5f, 2f)]
    public float radius;


    public LayerMask detectorLayerMask;
    int layerMask = (1 << 3) | (1 << 6);

    [Header("Debug parameters")]
    public Color debugIdleColor = Color.red;
    public Color debugDetectedColor = Color.green;
    public bool showGizmos = true;
    private void Awake()
    {
        anims = GameObject.Find("Player").GetComponent<PlayerAnimations>();
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anims.blockSensation = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Camera"))
        {
            if (collision.CompareTag("Player"))
            {
                MakeDamage(damage);
                Instantiate(PEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }


            if (collision.CompareTag("Enemy") && anims.blockSensation)
            {
                float finalDamage = damage * 1.5f;
                EnemiesLife eLife = collision.GetComponent<EnemiesLife>();
                eLife.life -= finalDamage;
                Destroy(this.gameObject);
            }
        }
    }

    private GameObject player;
    public GameObject Player
    {
        get => player;
        private set
        {
            player = value;
            playerDetected = player != null;
        }
    }

    private void Update()
    {
        

        if (playerDetected && Input.GetKeyDown("space"))
        {
            StartCoroutine(CounterBlock());
        }
        else
        {
            anims.blockSensation = false;
        }

        PerformDetect();
    }

    public void MakeDamage(float actualDamage)
    {
        //Calcula el da�o que hara gracias a la cantidad de calcio
        float finalDamage = damage / (pStats._calcio / 115f);
        pStats._pLife -= finalDamage;

        if(pStats._pLife <= 0)
        {
            pStats.Die();
        }

    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = debugIdleColor;
            if (playerDetected)
                Gizmos.color = debugDetectedColor;
            Gizmos.DrawSphere((Vector2)detectorOrigin.position, radius);
        }
    }

    public void PerformDetect()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)detectorOrigin.position, radius, detectorLayerMask);
        if (collider != null)
        {
            Player = collider.gameObject;
        }
        else
        {
            Player = null;
        }
    }


    IEnumerator CounterBlock()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.1f);
        anims.blockSensation = true;
        rb2d.constraints = RigidbodyConstraints2D.None;
        Vector3 direction = shooter.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.GetComponent<Rigidbody2D>().velocity = direction.normalized * 10;
    }
}
