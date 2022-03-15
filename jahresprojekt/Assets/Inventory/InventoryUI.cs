using System.Threading.Tasks;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory.onItemChangedCallback += UpdateUI;

        slots = GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (var item in slots)
        {
            item.ClearSlot();
        }

        for (int i = 0; i < inventory.Items.Count; i++)
        {
            try
            {
                if (inventory.Items[i] != null)
                {
                    slots[i].AddItem(inventory.Items[i]);
                }
            }
            catch (System.Exception) {
                
            }
            if (i >= slots.Length)
            {
                break;
            }
        }
        //des funktioniert afoch nit
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    if (i < inventory.Items.Count)
        //    {
        //        if (inventory.Items[i] != null)
        //        {
        //            slots[i].AddItem(inventory.Items[i]);
        //        }
        //    }
        //    else
        //    {
        //        slots[i].ClearSlot();
        //    }
        //}
    }
}
