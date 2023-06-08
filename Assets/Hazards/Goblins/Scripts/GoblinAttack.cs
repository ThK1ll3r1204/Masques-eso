using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    [SerializeField] public GoblinDetect detect;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject goblinRock;
    [SerializeField] public float moveSpeed;

    [SerializeField] float _bSpeed;
    [SerializeField] float _bCooldown;
    private float _bCooldowntimer;

    public bool isAttacking;
    public bool isChasing;
    [SerializeField] float aTimer;

    private float distance;




    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        detect = GetComponent<GoblinDetect>();
    }

    void Update()
    {
        if (detect.playerDetected && detect.distanceToPlayer > 5f && aTimer > 0.75f)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            isChasing = true;
        }
        else
            isChasing = false;

        if (detect.playerDetected && _bCooldowntimer <= 0)
        {
            ShootThePlayer();
        }

        if (detect.playerDetected && _bCooldowntimer <= 0.25f)
            isAttacking = true;

    }
    private void FixedUpdate()
    {
        //Cooldown del disparo
        if (_bCooldowntimer > 0f)
        {
            _bCooldowntimer -= Time.deltaTime;
        }

        if (isAttacking && aTimer < 0.8f)
        { 
            aTimer += Time.deltaTime;
        }

        if (aTimer > 0.75f)
        {
            isAttacking = false;   
        }
    }

    void ShootThePlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //anim.SetTrigger("Shoot");
        GameObject bullet = Instantiate(goblinRock, transform.position, Quaternion.identity, this.transform);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * _bSpeed;
        _bCooldowntimer = _bCooldown;
        aTimer = 0;
    } 
}
