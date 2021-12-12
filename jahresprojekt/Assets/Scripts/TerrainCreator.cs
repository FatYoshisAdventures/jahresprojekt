using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainCreator : MonoBehaviour
{

    [SerializeField]
    private SpriteShapeController shape;

    [SerializeField]
    private int numOfPoints = 30;

    [SerializeField]
    private float height = 5f;

    [SerializeField]
    private float deviation = 5f;

    [SerializeField]
    private float mapSize = 30;

    [SerializeField]
    private float roundness = 2f;

    private void Start()
    {
        ChangeMapSize();

        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        // calculate exact horizontal size of map
        float scale = shape.spline.GetPosition(2).x - shape.spline.GetPosition(1).x;

        // calculate what the distance between the points should be
        float distanceBtwnPoints = scale / numOfPoints;

        for (int i = 0; i < numOfPoints - 1; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBtwnPoints;
            shape.spline.InsertPointAt(i + 2, new Vector3(xPos, deviation * Mathf.PerlinNoise(i * height * Random.Range(0f, 1f), 0)));;
        }

        for (int i = 2; i < numOfPoints + 1; i++)
        {
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-roundness, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(+roundness, 0, 0));
        }
    }

    void ChangeMapSize()
    {
        //move points outwards to left
        shape.spline.SetPosition(0, shape.spline.GetPosition(0) - Vector3.right * mapSize);
        shape.spline.SetPosition(1, shape.spline.GetPosition(1) - Vector3.right * mapSize);

        //move points outwards to right
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * mapSize);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * mapSize);
    }
}
