using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find("player 1");
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        var rotation = Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen)));
        if (rotation.eulerAngles.z > 330)
        {
            rotation = Quaternion.Euler(new Vector3(0f, 0f, -30f));
        }
        else if (rotation.eulerAngles.z < 210)
        {
            rotation = Quaternion.Euler(new Vector3(0f, 0f, -150f));
        }
        rotation = Quaternion.Euler(0f, 0f, ClampAngle2(AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen), -155, -25));
        //Mathf.Clamp(rotation.eulerAngles.z, 360 - 150, 360 - 30);
        //Ta Daaa
        var angle = player.transform.rotation.eulerAngles.z;
        player.transform.rotation = Quaternion.Euler(0f,0f,ClampAngle(angle, -25, 25));
        rotation *= player.transform.rotation;

        transform.rotation = rotation;
        
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }
    
    private float ClampAngle2(float angle, float min, float max)
    {
        if (angle < 0 || angle > 180)
        {       // if angle in the critic region...
            
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360; //155
            if (min > 180) min -= 360; //25
        }
        if(angle > 90 && angle < 180)
        {
            Debug.Log("a");
            return -155;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        //Get the angle between the points
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}

