using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : MonoBehaviour
{
    [SerializeField] private int IncreaseAmount;
    [SerializeField] private bool RegenerateHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            //increases player max health, regeneration is handeld in health class
            collision.gameObject.GetComponent<Health>().IncreaseMaxHealthServerRpc(IncreaseAmount);

            //destroys this GameObject
            GameObject.Destroy(this.gameObject);
        }
    }
}
