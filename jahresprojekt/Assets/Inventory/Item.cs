using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory System/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    public virtual void Use()
    {
        Debug.Log($"Shooting with {name}");
        GameObject player = GameObject.Find("player");
        player.GetComponentInChildren<Shoot>().item = this;
    }
}
