using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rocket", menuName = "Inventory System/Item/Rocket")]
public class Rocket : Item
{
    public override void Use()
    {
        base.Use();

        Debug.Log("Shooting the rOCKET!!!");
    }
}
