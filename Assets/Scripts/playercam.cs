using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercam : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0,5f,-10f);

    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -10); // Camera follows the player with specified offset position
    }

    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

}
