using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    InventorySlot[] slots;

    public void Start()
    {
        inventory.onItemChangedCallback += UpdateUI;

        slots = GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Items.Count)
            {
                if (inventory.Items[i] != null)
                {
                    slots[i].AddItem(inventory.Items[i]);
                }
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
