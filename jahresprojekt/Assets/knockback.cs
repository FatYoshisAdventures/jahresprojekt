using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float forcestrenght = 5f;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 force = Vector2.zero;
        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.white);
        if (collision.contacts[0].normal == Vector2.left || collision.contacts[0].normal == Vector2.right)
        {
            force = new Vector2(forcestrenght * collision.contacts[0].point.x, 0);
        }
        else
        {
            return;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            GameObject go = collision.gameObject;
            rb.AddForceAtPosition(force, new Vector2(go.transform.position.x + go.transform.localScale.x * 1.05f * force.x, go.transform.position.y));
        }
    }
}
