using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    [SerializeField]
    private float speed;
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

        var anglePlayer = player.transform.rotation.eulerAngles.z;
        player.transform.rotation = Quaternion.Euler(0f,0f,ClampAngle(anglePlayer, -25, 25));

        var rotation = Quaternion.Euler(0f, 0f, ClampAngle(AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen), -155, -25));
        //rotation *= player.transform.rotation;
        
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
        if (angle > 90 && angle < 180) return min;
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

