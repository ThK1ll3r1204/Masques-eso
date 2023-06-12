using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;  // Prefab de la bala
    public float bulletSpeed = 10f;  // Velocidad de la bala
    public float explosionRadius = 2f;  // Radio del �rea de da�o
    public float explosionDuration = 3f;  // Duraci�n del �rea de da�o
    public float damageInterval = 1f;  // Intervalo de da�o al enemigo

    [SerializeField] float _bCooldown;
    [SerializeField] float _bCooldowntimer;


    [SerializeField] Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Disparar cuando se hace clic con el mouse
        if (Input.GetMouseButtonDown(0) && _bCooldowntimer <=0)
        {
            // Obtener la posici�n del mouse en el mundo
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Calcular la direcci�n de disparo hacia el mouse
            Vector2 direction = (mousePosition - transform.position).normalized;

            // Instanciar una nueva bala
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            // Disparar la bala en la direcci�n calculada
            bulletRigidbody.velocity = direction * bulletSpeed;

            //Controlar cadencia de disparo
            _bCooldowntimer = _bCooldown;

            // Asignar el script de explosi�n a la bala
            Explosion explosionScript = bullet.GetComponent<Explosion>();
            explosionScript.Initialize(explosionRadius, explosionDuration, damageInterval);
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
