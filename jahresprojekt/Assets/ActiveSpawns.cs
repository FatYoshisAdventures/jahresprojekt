using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpawns
{
    //Saves Active Spawn Points reset SpikeSpawn on Death, Triggering Spikes and Room Change
    public static Vector3 WorldSpawn; //set on specific action                  //done in SetSpawn atm maybe change to event
    public static Vector3 RoomSpawn;  //set on room change                      //not impemented in this project atm
    public static Vector3 SpikeSpawn; //set on entering certain area near spawn //done in SetSpawn
}
