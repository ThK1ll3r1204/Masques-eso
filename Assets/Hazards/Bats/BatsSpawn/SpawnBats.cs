using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBats : MonoBehaviour
{
    [SerializeField] GameObject bat;

    [SerializeField] float timer;
    [SerializeField] float maxTimer;

    [SerializeField] Transform firePoint;

    [SerializeField] float bullets;

    //CircleCast
    [SerializeField] bool IsSpawning;
    [SerializeField] LayerMask player;
    [SerializeField] float radius;

    
    void Start()
    {
        firePoint=GameObject.Find("firepoint").GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        IsSpawning = Physics2D.OverlapCircle(transform.position, radius, player);

        if(IsSpawning && timer >= maxTimer)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject bullet = Instantiate(bat, firePoint.position, Quaternion.identity);
        bullet.transform.position = firePoint.position;
        timer = 0;
    }

}
