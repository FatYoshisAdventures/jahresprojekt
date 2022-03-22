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
        set {
            if (IsServer)
            {
                if (int.Parse(value.ToString()) <= 0)
                {
                    GameObject.Destroy(this.gameObject, 0);
                    //Instatniate corpse
                    if (gameObject.tag.Equals("Player"))
                    {
                        //Finds animator on player death to kill player
                        FindObjectOfType<Canvas>().GetComponent<Animator>().SetTrigger("Dead");
                    }
                }
                else
                {
                    //sets health value between 0 and the maxamount of health
                    _Health = new NetworkVariable<int>(Mathf.Clamp(int.Parse(value.ToString()), 0, maxhealth));
                }
            }
        } 

    }

    void Start()
    {
        health = new NetworkVariable<int>(maxhealth);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>returns false if health is already full</returns>
    [ServerRpc]
    public void RegenerateHealthServerRpc(int amount)
    {
        if (int.Parse(health.ToString()) != maxhealth)
        {
            health = new NetworkVariable<int>(int.Parse(health.ToString()) + amount);
        }
    }

    [ServerRpc]
    public void IncreaseMaxHealthServerRpc(int increaseamount)
    {
        maxhealth += increaseamount;
        if (RegenerateOnMaxHealthIncrease == true)
        {
            health = new NetworkVariable<int>(int.Parse(health.ToString()) + increaseamount);
        }
    }

    [ServerRpc]
    public void DoDamageServerRpc(int damage)
    {
        health = new NetworkVariable<int>(int.Parse(health.ToString()) - damage);
    }
}
