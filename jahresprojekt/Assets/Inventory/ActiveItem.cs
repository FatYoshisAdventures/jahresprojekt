using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItem : MonoBehaviour
{
    public Image icon;
    public Item item;

    public void SetItem(Item newItem)
    {
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

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
}
