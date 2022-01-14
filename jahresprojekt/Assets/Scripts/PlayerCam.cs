using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Difference;

    private bool drag = false;

    [SerializeField]
    private GameObject player;

    //zoom
    [SerializeField] float minZoom = 1f;
    [SerializeField] float maxZoom = 10f;
    [SerializeField] float sensitivity = 2f;

    //bounds
    [SerializeField] float boundsLeft = -45f;
    [SerializeField] float boundsRight = 45f;
    [SerializeField] float boundsTop = 14f;
    [SerializeField] float boundsBottom = -7f;

    //offset of camera above player
    [SerializeField] float offset = 5f;

    Camera main;
    float zoomLevel;

    private void Start()
    {
        main = this.gameObject.GetComponent<Camera>();
        zoomLevel = main.orthographicSize;
    }

    private void LateUpdate()
    {
        Zoom();

        Pan();

        ReturnCamera();

        RestrictCameraPosition();
    }

    private void RestrictCameraPosition()
    {
        var newPoint = transform.position;

        //half the height of the camera box
        float height = main.orthographicSize;

        //half the width of the camera box
        float width = height * main.aspect;

        //restrict camera position between left and right boundaries
        if (newPoint.x - width < boundsLeft)
        {
            newPoint.x = Mathf.Clamp(newPoint.x - width, boundsLeft + width, boundsRight - width);
        }
        else if (newPoint.x + width > boundsRight)
        {
            newPoint.x = Mathf.Clamp(newPoint.x + width, boundsLeft + width, boundsRight - width);
        }

        //restrict camera position between bottom and top boundaries
        if (newPoint.y - height < boundsBottom)
        {
            newPoint.y = Mathf.Clamp(newPoint.y - height, boundsBottom + height, boundsTop - height);
        }
        else if (newPoint.y + height > boundsTop)
        {
            newPoint.y = Mathf.Clamp(newPoint.y + height, boundsBottom + height , boundsTop - height);
        }

        main.transform.position = newPoint;
    }

    private void Zoom()
    {
        //zoom on mouse scroll
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && Input.GetKey(KeyCode.LeftControl))
        {
            //multiply the zoom level by a factor for linear zoom
            zoomLevel *= 1 - (Input.GetAxis("Mouse ScrollWheel") * sensitivity);

            //clamp zoom level between min and max zoom
            zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);
            main.orthographicSize = zoomLevel;
        }
    }

    private void Pan()
    {
        //let the player drag the camera if he holds middle mouse button
        if (Input.GetMouseButton(2))
        {
            Difference = (main.ScreenToWorldPoint(Input.mousePosition)) - main.transform.position;

            //initial click on middle mouse button
            if (drag == false)
            {
                //set drag to true, keeps it true while the player holds the button
                drag = true;

                Origin = main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            main.transform.position = Origin - Difference;
        }
    }

    void ReturnCamera()
    {
        //return camera to player on right click
        //add offset to position so camera is above player
        if (Input.GetMouseButton(1))
        {
            main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offset * (main.orthographicSize / maxZoom), -10f);
        }
    }
}
