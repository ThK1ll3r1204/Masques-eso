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

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        GameObject bullet = Instantiate(bat, firePoint.position, Quaternion.identity);
        
        timer = 0;
    }

}
