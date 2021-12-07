using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
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

    private bool drag = false;

    [SerializeField]
    private GameObject player;

    private void LateUpdate()
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
            Camera.main.transform.position = Origin - Difference;
        }

        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = player.transform.position;
            Camera.main.transform.position += new Vector3(0,0,-10f);
        }
    }
}
