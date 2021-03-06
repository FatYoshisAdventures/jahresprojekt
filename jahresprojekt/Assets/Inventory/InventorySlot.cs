using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    Item item;

    public delegate void OnItemSelected(Item item);
    public OnItemSelected onItemSelectedCallback;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void SelectItem()
    {
        onItemSelectedCallback?.Invoke(this.item);
    }
}
