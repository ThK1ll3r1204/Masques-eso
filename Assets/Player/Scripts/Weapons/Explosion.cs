using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float explosionRadius = 3f;
    [SerializeField] float explosionDuration = 3f;
    [SerializeField] float damageInterval = 1f;
    [SerializeField] float lastDamageTime;

    public void Initialize(float radius, float duration, float interval)
    {
        explosionRadius = radius;
        explosionDuration = duration;
        damageInterval = interval;

        Invoke("Explode", 3f);
    }

    private void Explode()
    {
        // Instanciar un �rea de da�o
        GameObject explosion = new GameObject("Explosion");
        explosion.transform.position = transform.position;
        explosion.tag ="Molly";
        // Agregar un componente de c�rculo de colisi�n para el �rea de da�o
        CircleCollider2D collider = explosion.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = explosionRadius;

        // Destruir el �rea de da�o despu�s de la duraci�n especificada
        Destroy(explosion, explosionDuration);

        // Destruir la bala
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verificar si el objeto en el �rea de da�o es un enemigo
        if (other.CompareTag("Enemy"))
        {
            // Verificar el intervalo de tiempo desde el �ltimo da�o aplicado
            if (Time.time - lastDamageTime >= damageInterval)
            {
                // Causar da�o al enemigo
                EnemyLife enemy = other.GetComponent<EnemyLife>();
                enemy.ChangeLife(-1);

                // Actualizar el tiempo del �ltimo da�o aplicado
                lastDamageTime = Time.time;
            }
        }
    }
}

