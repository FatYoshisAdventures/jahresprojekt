using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=Tsha7rp58LI

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    public LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        lr.enabled = true;
        lr.SetPosition(0, startPoint);
        lr.SetPosition(1, endPoint);
    }

    public void EndLine()
    {
        lr.enabled = false;
    }
}
