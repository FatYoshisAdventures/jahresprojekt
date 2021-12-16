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

    [SerializeField] float minZoom = 1f;
    [SerializeField] float maxZoom = 15f;
    [SerializeField] float sensitivity= 2f;
    
    float zoomLevel;

    private void Start()
    {
        zoomLevel = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        Zoom();

        Pan();

        ReturnCamera();
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && Input.GetKey(KeyCode.LeftControl))
        {
            zoomLevel *= 1 + -Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);
            Camera.main.orthographicSize = zoomLevel;
        }
    }

    private void Pan()
    {
        if (Input.GetMouseButton(2))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;

            if (drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            var newPoint = Origin - Difference;

            newPoint.x = Mathf.Clamp(newPoint.x, -30, 30);
            newPoint.y = Mathf.Clamp(newPoint.y, 0, 25);

            Camera.main.transform.position = newPoint;
        }
    }

    void ReturnCamera()
    {

        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, -10f);
        }
    }
}
