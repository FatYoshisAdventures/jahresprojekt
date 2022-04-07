using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rocket", menuName = "Inventory System/Item/Rocket")]
public class Rocket : Item
{
    public GameObject rocket;

    public override void Use(Transform origin, Vector3 destination)
    {
        GameObject r = Instantiate(rocket, origin.position, origin.rotation) as GameObject;
        r.GetComponent<RocketBehaviour>().SetSpeed(destination);
    }
}
