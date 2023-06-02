using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    public CircleCollider2D body;
    public Animator animator;
    public LayerMask mapLayerMask;

    bool rightSideEmpty;
    bool leftSideEmpty;

    bool rightWall;
    bool leftWall;

    float timerStay;
    float maxTimerStay;

    float timerMove = 0;
    float maxTimerMove;

    float timerReset;
    float maxTimerReset = 0.01f;

    public float MoveSpeed;
    float sideToLook;
    float randomSide;

    [Header("RayCast Floor Config")]
    [SerializeField]
    [Range(0, 2f)]
    public float rayOffSet1;

    [Range(-2f, 0)]
    public float rayOffSet2;

    [Header("RayCast Wall Config")]
    [SerializeField]
    [Range(0, 2f)]
    public float rayWallOffSet1;

    [Range(-2f, 0)]
    public float rayWallOffSet2;


    [Range(-1, 2f)]
    public float rayWallOffSetHigh;


    [Header("Debug parameters")]
    public Color debugIdleColor = Color.red;
    public Color debugDetectedColor = Color.green;
    public bool showGizmos = true; 

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        maxTimerStay = Random.Range(2, 8);
        maxTimerMove = Random.Range(2, 5);
    }
    void Update()
    {
           if (timerStay > maxTimerStay)
           { 
                timerMove += Time.deltaTime;
                if (timerMove > maxTimerMove)
                {
                    timerReset = 0;
                    if (timerReset < maxTimerReset)
                    {
                        timerStay = 0;
                        timerMove = 0;
                        maxTimerStay = Random.Range(2, 8);
                        maxTimerMove = Random.Range(2, 5);
                        sideToLook = Random.Range(-1, 1);
                    }
                }

                if (timerMove < maxTimerMove)
                {
                     if (leftSideEmpty || leftWall)
                        sideToLook = 1;
                     else if (rightSideEmpty || rightWall)
                        sideToLook = -1;

                     Move(sideToLook);
                }
           }


           if (timerStay < maxTimerStay)
           {
               animator.SetBool("walking", false);

           }

        timerReset += Time.deltaTime;
        timerStay += Time.deltaTime;

        WallDetector();
        WithinFloorLimit();
    } 

    public void WithinFloorLimit()
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position + Vector3.right * rayOffSet2, Vector2.down, 2f, mapLayerMask);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position + Vector3.right * rayOffSet1, Vector2.down, 2f, mapLayerMask);

        if (raycastLeft.collider == null)
        {
            leftSideEmpty = true;
            animator.SetBool("walking", false);
        }
        else
            leftSideEmpty = false;


        if (raycastRight.collider == null)
        {
            rightSideEmpty = true;

            animator.SetBool("walking", false); 
        }
        else
            rightSideEmpty = false;       
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = debugIdleColor;
            if (leftWall || rightWall || leftSideEmpty || rightSideEmpty)
                Gizmos.color = debugDetectedColor;

            Gizmos.DrawRay(body.bounds.center + Vector3.right * rayOffSet1, Vector2.down * 2f);
            Gizmos.DrawRay(body.bounds.center + Vector3.right * rayOffSet2, Vector2.down * 2f);


            Gizmos.DrawRay(body.bounds.center + (Vector3.right * rayWallOffSet2) + (Vector3.up * rayWallOffSetHigh), Vector2.down * 2f);
            Gizmos.DrawRay(body.bounds.center + (Vector3.right * rayWallOffSet1) + (Vector3.up * rayWallOffSetHigh), Vector2.down * 2f); 
        }
    }


    public void WallDetector()
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position + (Vector3.right * rayWallOffSet2) + (Vector3.up * rayWallOffSetHigh), Vector2.down, 2f, mapLayerMask);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position + (Vector3.right * rayWallOffSet1) + (Vector3.up * rayWallOffSetHigh), Vector2.down, 2f, mapLayerMask);

        if (raycastLeft.collider != null)
            leftWall = true;
        else
            leftWall = false;

        if (raycastRight.collider != null)
            rightWall = true;
        else
            rightWall = false;
    }

    void Move(float lookAt)
    {
        if (lookAt < 0)
        {
            transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("walking", true);
        }
        else
        {
            transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("walking", true);
        }
    }
}
