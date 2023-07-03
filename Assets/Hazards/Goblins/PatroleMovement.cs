using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroleMovement : MonoBehaviour
{
    public float timerStay;
    public float maxTimerStay;

    public float timerMove = 0;
    public float maxTimerMove;

    float timerReset;
    float maxTimerReset = 0.01f;

    public float MoveSpeed;
    public int sideToLook;
    float randomSide;

    public bool isWalking = false;

    GoblinDetect goblinDetect;
    GoblinAttack gAttack;

    Transform direction;
    private void Start()
    {
        gAttack = GetComponent<GoblinAttack>();
        goblinDetect = GetComponent<GoblinDetect>();
        direction = this.transform.Find("directionInPatrole");
        maxTimerStay = Random.Range(2, 12);
        maxTimerMove = Random.Range(2, 5);
    }
    void Update()
    {
        if (!goblinDetect.playerFocused)
        { 
            if (timerStay > maxTimerStay)
            {
                timerMove += Time.deltaTime;
                if (timerMove > maxTimerMove)
                {
                    //anim.SetBool("Idle", true);
                    //anim.SetBool("Walking", false);
                    timerReset = 0;
                    if (timerReset < maxTimerReset)
                    {
                        timerStay = 0;
                        timerMove = 0;
                        maxTimerStay = Random.Range(2, 12);
                        maxTimerMove = Random.Range(2, 5);
                        sideToLook = Random.Range(-2, 2);
                    }
                }

                if (timerMove < maxTimerMove)
                {
                    GoBackThereisAWall(goblinDetect.wallXrDetected, -2);
                    GoBackThereisAWall(goblinDetect.wallXlDetected, 2);
                    GoBackThereisAWall(goblinDetect.wallYuDetected, -1);
                    GoBackThereisAWall(goblinDetect.wallYdDetected, 1);
                    Move(sideToLook);
                }
            }

            if (timerMove == 0)
                isWalking = false;


            timerReset += Time.deltaTime;
            timerStay += Time.deltaTime;
        }
        else if (gAttack.isChasing)
            isWalking = false;

    }

    private void GoBackThereisAWall(bool sideDetected, int newSide)
    {
        if (sideDetected)
        {
            sideToLook = newSide;
        }
    }

    void Move(float lookAt)
    {
        if(!goblinDetect.playerFocused)
        {
            isWalking = true;
            switch (lookAt)
            {
                case -2:
                    direction.position = this.transform.position + Vector3.left * 3;
                    break;
                case 2:
                    direction.position = this.transform.position + Vector3.right * 3;
                    break;
                case 1:
                    direction.position = this.transform.position + Vector3.up * 3;
                    break;
                case -1:
                    direction.position = this.transform.position + Vector3.down * 3;
                    break;

            }
            transform.position = Vector2.MoveTowards(this.transform.position, direction.position, MoveSpeed * Time.deltaTime);
        }
    }
}
