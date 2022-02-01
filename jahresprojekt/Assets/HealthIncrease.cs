using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : MonoBehaviour
{
    [SerializeField] private int IncreaseAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            //increases player max health
            collision.gameObject.GetComponent<Health>().IncreaseMaxHealth(IncreaseAmount);

            //adds health and instantly regenerates it
            collision.gameObject.GetComponent<Health>().health += IncreaseAmount;

            //destroys this GameObject
            GameObject.Destroy(this.gameObject);
        }
    }
}
