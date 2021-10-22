using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class please : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen)));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        //Get the angle between the points
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}

