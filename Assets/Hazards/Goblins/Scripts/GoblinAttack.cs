using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    [SerializeField] public GoblinDetect detect;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject goblinRock;
    [SerializeField] public float moveSpeed;
    [SerializeField] public Animator anim;

    [SerializeField] float _bSpeed;
    [SerializeField] float _bCooldown;
    private float _bCooldowntimer;

    private float distance;




    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        detect = GetComponent<GoblinDetect>();
    }

    void Update()
    {
        if (detect.playerDetected && detect.distanceToPlayer > 5f)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        if (detect.playerDetected && _bCooldowntimer <= 0)
        {
            ShootThePlayer();
            anim.SetTrigger("Shoot");
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

    void ShootThePlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(goblinRock, transform.position, Quaternion.identity, this.transform);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * _bSpeed;
        _bCooldowntimer = _bCooldown;
    } 
}
