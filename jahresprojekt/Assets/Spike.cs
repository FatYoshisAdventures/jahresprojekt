using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int SpikeDamage;
    [SerializeField] private bool ResetToBeginingOfRoom;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //spikes hit
        if (collision.gameObject.CompareTag("Player"))
        {
            //DoDamage
            collision.gameObject.GetComponent<Health>().health -= SpikeDamage;

            if (ResetToBeginingOfRoom == true)
            {
                collision.gameObject.transform.position = ActiveSpawns.RoomSpawn;
            }
            else
            {
                //if no respawn point was hit go to beginning of room
                if (ActiveSpawns.SpikeSpawn == null)
                {
                    collision.gameObject.transform.position = ActiveSpawns.RoomSpawn;
                }
                collision.gameObject.transform.position = ActiveSpawns.SpikeSpawn;
            }
        }
    }
}
