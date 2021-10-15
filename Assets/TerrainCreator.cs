using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainCreator : MonoBehaviour
{

    [SerializeField]
    private SpriteShapeController shape;

    [SerializeField]
    private int numOfPoints = 10;

    private int scale;
    private float distanceBtwnPoints;

    void Start()
    {
        float scale =  - shape.spline.GetPosition(2).x;
        shape.spline.SetPosition(2, shape.spline.GetPosition(2).x + Vector3.right * 100);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3).x + Vector3.right * 100);

        distanceBtwnPoints = scale / numOfPoints;

        for (int i = 0; i < numOfPoints; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBtwnPoints;
            shape.spline.InsertPointAt(i+2, new Vector3(xPos, Random.Range(0f, 2f)));
        }
    }
}
