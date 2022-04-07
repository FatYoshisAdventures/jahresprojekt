using UnityEngine;

public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    public virtual void Use()
    {

    }

    public virtual void Use(Transform origin, Vector3 destination)
    {

    }

    public void Select()
    {

    }
}
