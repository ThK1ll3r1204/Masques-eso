using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShoot : MonoBehaviour
{

    public GameObject enemyBullet;
    public float TimeA;
    public float TimeB;

    private int lastDir;

    public float bSpeed;

    public bool triggerdetect;
    public LayerMask TriggerLayer;
    public float distance;


    void Update()
    {
        Trap();
        TimeA += Time.deltaTime;
    }

    void Trap()
    {
        triggerdetect = Physics2D.Raycast(transform.position, -transform.up, distance, TriggerLayer);
        if (triggerdetect && TimeA > TimeB)
        {
            Vector3 direction = Vector3.zero;
            direction = Vector3.down;
            GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bSpeed;
            TimeA = 0;
            TimeB = Random.Range(0.5f, 3f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}