using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBats : MonoBehaviour
{
    [SerializeField] GameObject bat;

    [SerializeField] float timer;
    [SerializeField] float maxTimer;

    [SerializeField] Transform firePoint;
    [SerializeField] int batsPerSpawn;
    Transform detectorOrigin;
    [SerializeField] float radiusForEnemies;
    [SerializeField] LayerMask enemiesLayerMask;
    int enemiesinarea;
    [SerializeField] int maxEnemiesAmount;

    //CircleCast
    [SerializeField] bool IsSpawning;
    [SerializeField] LayerMask player;
    [SerializeField] float radius;

    
    void Start()
    {
        batsPerSpawn = 5;
        detectorOrigin = GetComponent<Transform>();
        maxTimer = 2.75f;
        firePoint=GameObject.Find("firepoint").GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        IsSpawning = Physics2D.OverlapCircle(transform.position, radius, player);

        Collider2D[] enemiesAmount = Physics2D.OverlapCircleAll((Vector2)detectorOrigin.position, radiusForEnemies, enemiesLayerMask);

        if (enemiesAmount.Length >= maxEnemiesAmount)
            IsSpawning = false;

        enemiesinarea = enemiesAmount.Length;

        if (IsSpawning)
        {
            IsSpawning = true;

            timer -= Time.deltaTime;

            if (timer <= 0 && IsSpawning && batsPerSpawn>0)
            {
                GameObject bats = Instantiate(bat, firePoint.transform.position, Quaternion.identity);
                timer = maxTimer;
                batsPerSpawn--;
                if (batsPerSpawn <= 0)
                {
                    batsPerSpawn = 0;

                    IsSpawning = false;
                }


            }            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, radiusForEnemies);
    }

}
