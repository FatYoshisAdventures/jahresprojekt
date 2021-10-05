using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercam : MonoBehaviour
{
    //public Transform player;
    //public Vector3 offset = new Vector3(0,5f,-10f);

    //void Update()
    //{
    //    transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -10); // Camera follows the player with specified offset position
    //}

    //void Start()
    //{
    //    //offset = transform.position - player.transform.position;
    //}


    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;

    private bool drag = false;


    private void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
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
            Camera.main.transform.position = Origin - Difference;
        }

        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = ResetCamera;
        }
    }

}
