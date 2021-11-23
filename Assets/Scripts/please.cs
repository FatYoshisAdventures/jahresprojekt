using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class please : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float minRotation = -10;

    [SerializeField]
    private float maxRotation = -170;

    private float thresholdRotation;

    private void Start()
    {
        thresholdRotation = (minRotation + maxRotation) / 2;
    }

    void Update()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        var angle = player.transform.eulerAngles.z;
        angle = (angle > 180) ? angle - 360 : angle;

        var rotation = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);
        Debug.Log(rotation + angle);
        
        if (rotation < minRotation + angle && rotation >= thresholdRotation + angle)
        {
            rotation = minRotation + angle;
        }
        else if (rotation > maxRotation + angle && rotation < thresholdRotation + angle)
        {
            rotation = maxRotation + angle;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        //Get the angle between the points
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}

