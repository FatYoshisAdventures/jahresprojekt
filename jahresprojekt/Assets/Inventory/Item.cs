using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory System/Item")]
public abstract class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    public abstract void Use();
}
