using UnityEngine;

public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    public virtual void Use()
    {

    }

    //public virtual void Use(Transform origin, Vector3 destination, ulong clientid)
    //{

    //}
    
    public virtual void Use(Vector3 origionpos, Quaternion origionrot, Vector3 destination, ulong clientid)
    {

    }

    public void Select()
    {

    }
}
