using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatXplode : MonoBehaviour
{
    [SerializeField] float speed = 3f;           // Velocidad de movimiento del enemigo
    [SerializeField] float chaseRange = 5f;      // Rango de persecución del enemigo
    [SerializeField] float explosionRange = 1f;  // Rango de explosión del enemigo
    
    [SerializeField] GameObject explosionPrefab; // Prefab de la explosión

    [SerializeField] Transform player;          // transform del jugador
    [SerializeField] Rigidbody2D rb;

    [SerializeField] bool hasExploded = false;  // controlar si la explosión ya ha ocurrido

    [SerializeField] bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calcula la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;

            // Mueve el enemigo hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        }

        else
        {
            isChasing = false;
        }

        if (distanceToPlayer <= explosionRange)
        {
            Explode();
        }

        

    }

    private void Explode()
    {
        Debug.Log("Explotó");

        if (hasExploded)  // Verificar si la explosión ya ha ocurrido
            return;

        hasExploded = true;

        speed = 0;
        // daño al jugador xd
        PlayerStats playerLife = player.GetComponent<PlayerStats>();
        if (player != null)
        {
            playerLife.TakeDamage(-20f);
        }

        // Instanciar la explosión
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);


        Destroy(gameObject, 0.2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 15f);
        Gizmos.DrawWireSphere(transform.position, 4f);
    }
}
