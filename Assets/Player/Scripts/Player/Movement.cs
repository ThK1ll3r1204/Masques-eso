using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Jose
    public Collider2D _coll;
    public Rigidbody2D _rb;
    [SerializeField] float _speed;
    public bool isWalking = true;

    public PlayerStats pStats;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        pStats = this.GetComponent<PlayerStats>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (!pStats.isDead)
        {
            Vector2 _move = new Vector2(horizontal, vertical).normalized;
            _rb.velocity = _move * _speed;


            if (horizontal != 0f || vertical != 0f)
                isWalking = true;
            else
                isWalking = false;

            

        }
    }
}
