using UnityEngine;

public class TrapShoot : MonoBehaviour
{

    public GameObject enemyBullet;
    public float TimeA;
    public float TimeB;
    [SerializeField] Transform firePoint;
    public float bSpeed;

    [SerializeField] Animator anim;


    private void Start()
    {
        firePoint=GetComponentInChildren<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        Trap();
        TimeA += Time.deltaTime;
    }

    void Trap()
    {
        if (TimeA > TimeB)
        {
            Vector3 direction = Vector3.zero;
            direction = Vector3.down;
            anim.SetTrigger("Shoot");
            GameObject bullet = Instantiate(enemyBullet, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bSpeed;
            TimeA = 0;
            TimeB = Random.Range(0.5f, 3f);
        }
        
    }
       
}