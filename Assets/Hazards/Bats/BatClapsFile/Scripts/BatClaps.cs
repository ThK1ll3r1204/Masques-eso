using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatClaps : MonoBehaviour
{
    [SerializeField] float speed;           // Velocidad de movimiento del enemigo
    [SerializeField] float chaseRange = 5f;      // Rango de persecución del enemigo

    [SerializeField] Transform jugador;          // transform del jugador
    [SerializeField] Animator anim;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] bool isChasing = false;

    public Transform detectorOrigin;
    public float radius;
    public LayerMask detectorLayerMask;
    public bool counterShot;

    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float timer;
    [SerializeField] float maxTimer = 3f;

    public bool playerDetected { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

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

    // Update is called once per frame
    void Update()
    {
        

        if (Vector2.Distance(transform.position, jugador.position) <= detectionRadius)
        {
            timer += Time.deltaTime;

            // Verificar si es el momento de disparar
            if (timer>=maxTimer)
            {

                Shoot();
                timer = 0;
            }
        }
        else
        {
            anim.SetBool("Shoot", false);
        }

        PerformDetect();
    }

    //private void Move()
    //{
    //    // Calcula la distancia entre el enemigo y el jugador
    //    float distanceToPlayer = Vector2.Distance(transform.position, jugador.position);

    //    if (distanceToPlayer <= chaseRange)
    //    {
    //        isChasing = true;

    //        // Mueve el enemigo hacia el jugador
    //        Vector2 direction = (jugador.position - transform.position).normalized;
    //        transform.position = Vector2.MoveTowards(this.transform.position, jugador.transform.position, speed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        isChasing = false;
    //    }

    //}

    private void Shoot()
    {

        anim.SetBool("Shoot", true);
        // Disparar en las cuatro diagonales
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 direction in directions)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * 5f;
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
}
