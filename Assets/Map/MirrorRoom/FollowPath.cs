using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPath : MonoBehaviour
{
    [SerializeField] GameObject PresionaE;
    public Path waypoints;
    public float speed = 3f;

    public float t = 0;
    public int startPoint;
    public int endPoint;

    public float timer;
    public float Maxtimer;

    private bool interactPressed = false;
    private bool secondInteract = false;

    public int nextSceneIndex;

    private void Awake()
    {
        PresionaE  = GameObject.Find("PresionaE");
    }
    // Start is called before the first frame update
    void Start()
    {
        startPoint = 0;
        endPoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactPressed)
        {
            MoveOnPath();
            PresionaE.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "Lore")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (secondInteract)
                {
                    SceneManager.LoadScene("Tutorial");
                }
                else
                {
                    interactPressed = true;
                    secondInteract = true;
                }
            }
        }

        timer += Time.deltaTime;
        if (timer >= Maxtimer)
        {
            speed = 1.3f;
            timer = 0;
            Maxtimer = 0.5f;
        }
    }

    void MoveOnPath()
    {
        Vector3 startPosition = waypoints.GetPoint(startPoint);
        Vector3 endPosition = waypoints.GetPoint(endPoint);

        transform.position = Vector3.Lerp(startPosition, endPosition, t);

        t += speed * Time.deltaTime / Vector3.Distance(startPosition, endPosition);

        if (t >= 1f)
        {
            t = 0;

            startPoint++;
            endPoint++;

            if (endPoint >= waypoints.PointsCount())
            {
                waypoints.Reverse();
                startPoint = 0;
                endPoint = 1;
            }

            interactPressed = false;
        }
    }
}
