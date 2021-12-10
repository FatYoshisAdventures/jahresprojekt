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

    [SerializeField]
    private float deviation = 2f;

    private void Start()
    {
        shape.spline.SetPosition(0, shape.spline.GetPosition(0) - Vector3.right * 10);
        shape.spline.SetPosition(1, shape.spline.GetPosition(1) - Vector3.right * 10);
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * 10);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * 10);

        float scale = shape.spline.GetPosition(2).x - shape.spline.GetPosition(1).x;

        float distanceBtwnPoints = scale / numOfPoints;

        for (int i = 0; i < numOfPoints-1; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBtwnPoints;
            shape.spline.InsertPointAt(i + 2, new Vector3(xPos, Random.Range(0f, deviation)));
        }

        for (int i = 2; i < numOfPoints+1; i++)
        {
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-1, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(1, 0, 0));

        }
    }
}
