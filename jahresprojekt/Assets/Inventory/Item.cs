using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName ="Iventory System/Items/Weapon")]
public class Item : ScriptableObject
{
    public GameObject prefab;

    [TextArea(15, 20)]
    public string description;
}
