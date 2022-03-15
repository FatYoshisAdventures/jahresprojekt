using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject[] bullets;
    [SerializeField] private Inventory inventory;
    public Item item;
    private int index;

    void Update()
    {
        //return if on ui-element
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        //activate on left click
        if (Input.GetMouseButtonDown(0))
        {
            string itemname = "DEFAULT";
            if (item != null)
            {
                itemname = item.name;
            }

            index = itemname switch
            {
                "Rocket" => 1,
                _ => 0,
            };
            Instantiate(bullets[index], this.transform.position, this.transform.rotation);
            if (item != null)
            {
                inventory.RemoveItem(item);
                item = null;
            }
        }
    }
}