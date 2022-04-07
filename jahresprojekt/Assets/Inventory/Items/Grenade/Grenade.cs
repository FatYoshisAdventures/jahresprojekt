using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grenade", menuName = "Inventory System/Item/Grenade")]
public class Grenade : Item
{
    public GameObject rocket;

    public override void Use(Transform t)
    {
        Instantiate(rocket, t.position, t.rotation);
    }
}
