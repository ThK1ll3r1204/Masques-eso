using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroleMovement : MonoBehaviour
{
    float timerStay;
    float maxTimerStay;

    float timerMove = 0;
    float maxTimerMove;

    float timerReset;
    float maxTimerReset = 0.01f;

    public float MoveSpeed;
    float sideToLook;
    float randomSide;

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
                     Move(sideToLook);
                }
           }

        timerReset += Time.deltaTime;
        timerStay += Time.deltaTime;

    } 



   

    void Move(float lookAt)
    {
        if (lookAt < 0)
        {
            transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
