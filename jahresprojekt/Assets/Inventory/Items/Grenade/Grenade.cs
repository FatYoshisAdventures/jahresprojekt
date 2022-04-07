using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grenade", menuName = "Inventory System/Item/Grenade")]
public class Grenade : Item
{
    public GameObject grenade;

    public override void Use(Vector3 originpos, Quaternion originrot, Vector3 destination, ulong clientid)
    {
        GameObject g = Instantiate(grenade, originpos, originrot) as GameObject;
        g.GetComponent<GrenadeBehaviour>().SetSpeed(destination);
    }
}
