using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Health : NetworkBehaviour

{
    [SerializeField] private bool RegenerateOnMaxHealthIncrease;
    public int maxhealth = 3;

    [SerializeField] NetworkVariable<int> _Health = new NetworkVariable<int>(0);

    [HideInInspector]
    public NetworkVariable<int> health
    {
        get { return _Health; }
        set
        {
            if (IsServer)
            {
                if (value.Value <= 0)
                {
                    //plays animation for client, deletes gameobject on clients and server
                    onDeathClientRpc();
                }
                else
                {
                    //sets health value between 0 and the maxamount of health
                    _Health = new NetworkVariable<int>(Mathf.Clamp(value.Value, 0, maxhealth));
                }
            }
        }

    }

    void Start()
    {
        if (IsOwner)
        {
            RegenerateHealthServerRpc(maxhealth);
        }
        //health = new NetworkVariable<int>(maxhealth);
    }

    [ClientRpc]
    void onDeathClientRpc()
    {
        if (gameObject.tag.Equals("Player") && IsOwner)
        {
            //Finds animator on player death to kill player
            FindObjectOfType<Canvas>().GetComponent<Animator>().SetTrigger("Dead");
        }
        GameObject.Destroy(this.gameObject, 0);
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    public void DestroyGameObjectServerRpc(int amount)
    {
        GameObject.Destroy(this.gameObject, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>returns false if health is already full</returns>
    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    public void RegenerateHealthServerRpc(int amount)
    {
        if (health.Value != maxhealth)
        {
            health = new NetworkVariable<int>(health.Value + amount);
        }
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    public void IncreaseMaxHealthServerRpc(int increaseamount)
    {
        maxhealth += increaseamount;
        if (RegenerateOnMaxHealthIncrease == true)
        {
            health = new NetworkVariable<int>(health.Value + increaseamount);
        }
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    public void DoDamageServerRpc(int damage)
    {
        health = new NetworkVariable<int>(health.Value - damage);
    }
}
