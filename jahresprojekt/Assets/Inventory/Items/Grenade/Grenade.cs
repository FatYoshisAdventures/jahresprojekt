using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grenade", menuName = "Inventory System/Item/Grenade")]
public class Grenade : Item
{
    public GameObject grenade;

    public override void Use(Transform origin, Vector3 destination)
    {
        GameObject g = Instantiate(grenade, origin.position, origin.rotation) as GameObject;
        g.GetComponent<GrenadeBehaviour>().SetSpeed(destination);
    }
}
