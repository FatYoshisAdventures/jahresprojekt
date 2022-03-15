using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=_IqTeruf3-s

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class Inventory : ScriptableObject
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> Items = new();
    public void AddItem(Item item)
    {
        bool hasItem = false;

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] == item)
            {
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Items.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
    }

    public void RemoveItem(Item item) 
    {
        Items.Remove(item); 
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}