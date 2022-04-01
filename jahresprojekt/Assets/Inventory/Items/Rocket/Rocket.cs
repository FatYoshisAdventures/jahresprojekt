using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rocket", menuName = "Inventory System/Item/Rocket")]
public class Rocket : Item
{
    public GameObject rocket;

    public override void Use(Transform t)
    {
        Instantiate(rocket, t.position, t.rotation);
    }
}
