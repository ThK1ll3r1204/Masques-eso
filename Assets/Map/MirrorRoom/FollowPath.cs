using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Path waypoints;
    public float speed = 3f;

    public float t = 0;
    public int startPoint;
    public int endPoint;

    public float timer;
    public float Maxtimer;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = 0;
        endPoint = 1;
    }

    // Update is called once per frame
    void Update()
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
        }

        timer += Time.deltaTime;
        if(timer>= Maxtimer)
        {
            speed = 1f;
            timer = 0;
            Maxtimer = 0.5f;
        }
    }
}
