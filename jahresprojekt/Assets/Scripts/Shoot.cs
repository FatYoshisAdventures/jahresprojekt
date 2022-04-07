using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.Netcode;


public class Shoot : NetworkBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ActiveItem activeItem;
    [SerializeField] private Camera camera;

    [SerializeField] private TrajectoryLine tl;
    
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float maxVolume = 0.1f;
    [SerializeField] private float reloadtime = 0.75f;
    [SerializeField] private float volume = 0.5f;


    private int index;
    private bool reloaded;

    private void Awake()
    {
        reloaded = true;
    }

    void Update()
    {
        //return if not owner
        if (!this.GetComponentInParent<NetworkObject>().IsOwner) return;

        if (Input.GetMouseButtonUp(0))
        {
            tl.EndLine();
        }

        

        //return if on ui-element
        if (EventSystem.current.IsPointerOverGameObject()) return;

        //return if no item is selected
        if (activeItem.item == null) return;

        //while left click
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            tl.RenderLine(this.transform.position, currentPoint, reloaded);
            //Debug.Log(this.transform.position + " " + currentPoint);
        }

        Debug.Log("bitte");
        
        //on leaving left click
        if (Input.GetMouseButtonUp(0) && reloaded)
        {
            
            StartCoroutine(Shooting());
            StartCoroutine(Reload());
        }
    }

    IEnumerator Shooting()
    {
        Vector3 destination = camera.ScreenToWorldPoint(Input.mousePosition);

        yield return new WaitForSeconds(delay);
        //activeItem.item.Use(this.transform, destination, NetworkObject.OwnerClientId);
        UseItemServerRpc(this.GetComponentInParent<NetworkObject>().OwnerClientId, this.transform.position, this.transform.rotation, destination, inventory.Items.IndexOf(activeItem.item));
    }

    //ensures that server is responsible so object can be successfully spawned later
    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    public void UseItemServerRpc(ulong ownerclientID, Vector3 transformpos, Quaternion transformrot, Vector3 position, int index)
    {
        activeItem.item = inventory.Items[index];
        activeItem.item.Use(transformpos, transformrot, position, ownerclientID);
    }

    

    IEnumerator Reload()
    {
        reloaded = false;
        yield return new WaitForSeconds(reloadtime + delay);
        reloaded = true;
    }
}
