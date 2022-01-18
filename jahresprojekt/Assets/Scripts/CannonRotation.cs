using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera main;

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
        Vector2 positionOnScreen = main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = main.ScreenToViewportPoint(Input.mousePosition);

        //Normalize rotation between -180 +180
        var angle = player.transform.eulerAngles.z;
        if (angle > 180) angle -= 360;

        var rotation = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);

        //Normalize rotation between -180 and +180
        var newRotation = rotation - angle;
        if (newRotation > 180) newRotation -= 360;
        if (newRotation < -180) newRotation += 360;

        //Clamp rotation between min and max rotation
        if (newRotation < minRotation && newRotation >= thresholdRotation)
        {
            rotation = minRotation + angle;
        }
        else if (newRotation > maxRotation && newRotation < thresholdRotation)
        {
            rotation = maxRotation + angle;
        }

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        //Get the angle between the points
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}