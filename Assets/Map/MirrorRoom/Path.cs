using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> points;

    void OnDrawGizmos()
    {
        if (points.Count != transform.childCount)
        {
            points.Clear();
            points.AddRange(GetComponentsInChildren<Transform>());
            points.RemoveAt(0);
        }


        for (int i = 1; i < points.Count; i++)
            Debug.DrawLine(points[i - 1].position, points[i].position, Color.green);
    }


    public Vector3 GetPoint(int index)
    {
        return points[index].position;
    }

    public int PointsCount()
    {
        return points.Count;
    }

    public void Reverse()
    {
        points.Reverse();
    }
}
