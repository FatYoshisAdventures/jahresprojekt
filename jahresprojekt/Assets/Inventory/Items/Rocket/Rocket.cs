using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[CreateAssetMenu(fileName = "New Rocket", menuName = "Inventory System/Item/Rocket")]
public class Rocket : Item
{
    public GameObject rocket;

    public override void Use(Vector3 originpos, Quaternion originrot, Vector3 destination, ulong clientid)
    {
        Debug.Log("test");
        //creates rocket on server
        CreateRocketServerRpc(originpos, originrot, destination, clientid);

        //creates rocket on client
        //GameObject r = Instantiate(rocket, origin.position, origin.rotation) as GameObject;
        //r.GetComponent<RocketBehaviour>().SetSpeed(destination);
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    private void CreateRocketServerRpc(Vector3 originpos, Quaternion originrot, Vector3 destination, ulong clientid)
    {
        GameObject r = Instantiate(rocket, originpos, originrot) as GameObject;
        r.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientid);
        r.GetComponent<RocketBehaviour>().SetSpeed(destination);
        
    }
}
