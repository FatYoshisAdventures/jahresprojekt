using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[CreateAssetMenu(fileName = "New Grenade", menuName = "Inventory System/Item/Grenade")]
public class Grenade : Item
{
    public GameObject grenade;

    public override void Use(Vector3 originpos, Quaternion originrot, Vector3 destination, ulong clientid)
    {
        //creates rocket on server
        CreateRocketServerRpc(originpos, originrot, destination, clientid);

    }

    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    private void CreateRocketServerRpc(Vector3 originpos, Quaternion originrot, Vector3 destination, ulong clientid)
    {
        GameObject r = Instantiate(grenade, originpos, originrot) as GameObject;
        r.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientid);
        r.GetComponent<RocketBehaviour>().SetSpeed(destination);

    }
}
