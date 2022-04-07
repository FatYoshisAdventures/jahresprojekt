using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=Tsha7rp58LI

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    public LineRenderer lr;

    [SerializeField] Color colorReady;
    [SerializeField] Color colorStandby;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint, bool ready)
    {
        lr.enabled = true;

        //set gradient, if ready draw white, if not draw red
        Gradient gradient = new Gradient()
        {
            alphaKeys = new GradientAlphaKey[]
            {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(1f, 1f)
            },
            colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(Color.clear, 0),
                new GradientColorKey(ready ? colorReady : colorStandby, 1)
            }
        };
        lr.colorGradient = gradient;

        //set start and end position of trajectory
        lr.SetPosition(0, startPoint);
        lr.SetPosition(1, endPoint);
    }

    public void EndLine()
    {
        lr.enabled = false;
    }
}
