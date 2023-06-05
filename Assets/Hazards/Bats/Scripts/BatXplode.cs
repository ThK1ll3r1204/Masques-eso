using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatXplode : MonoBehaviour
{
    [SerializeField] float speed;           // Velocidad de movimiento del enemigo
    [SerializeField] float chaseRange = 5f;      // Rango de persecución del enemigo
    [SerializeField] float explosionRange = 1f;  // Rango de explosión del enemigo

    [SerializeField] float xplodeTimer;
    [SerializeField] float maxXplodeTimer;

    [SerializeField] GameObject explosionPrefab; // Prefab de la explosión
    [SerializeField] Animator anim;
    [SerializeField] Transform jugador;          // transform del jugador
    [SerializeField] Rigidbody2D rb;

    [SerializeField] bool hasExploded = false;  // controlar si la explosión ya ha ocurrido

    [SerializeField] bool isChasing = false;



    public Transform detectorOrigin;
    public float radius;
    public LayerMask detectorLayerMask;
    public bool counterShot;


    public bool playerDetected { get; private set; }


    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        counterShot = false;

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

    void Update()
    {
        // Calcula la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, jugador.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;

            // Mueve el enemigo hacia el jugador
            Vector2 direction = (jugador.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(this.transform.position, jugador.transform.position, speed * Time.deltaTime);
        }

        else
        {
            isChasing = false;
        }

        if (distanceToPlayer <= explosionRange)
        {
            xplodeTimer += Time.deltaTime;

            if (xplodeTimer >= maxXplodeTimer)
            {
                anim.SetBool("PreBoom", true);
                
                Explode();
            }
            else { anim.SetBool("PreBoom", false); }

        }

        if (playerDetected && Input.GetKeyDown("space"))
        {
            counterShot = true;
        }

        PerformDetect();

    }

    private void Explode()
    {
        

        if (hasExploded)  // Verificar si la explosión ya ha ocurrido
            return;

        hasExploded = true;
        speed = 0;

        // Instanciar la explosión
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        anim.SetBool("PreBoom", false);
        anim.SetBool("Boom", true);

        // daño al jugador xd
        PlayerStats playerLife = player.GetComponent<PlayerStats>();
        if (player != null && !counterShot)
        {
            playerLife.TakeDamage(-20f);
        }

        

        Destroy(gameObject, 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 15f);
        Gizmos.DrawWireSphere(transform.position, 2f);
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


   


}
