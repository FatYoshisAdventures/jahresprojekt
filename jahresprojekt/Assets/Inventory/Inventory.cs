using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=_IqTeruf3-s


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class Inventory : ScriptableObject
{
    public List<InventorySlot> Container = new();
    public void AddItem(Item item)
    {
        bool hasItem = false;

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == item)
            {
                hasItem = true;
                break;
            }
        }

        if(!hasItem)
        {
            Container.Add(new InventorySlot(item));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;

    public InventorySlot(Item item)
    {
        this.item = item;        
    }
}
