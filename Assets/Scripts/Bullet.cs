using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float explosionradius = 20f;

    [SerializeField]
    private float explosionforce = 200f;

    private void Start()
    {
        rb.velocity = transform.right * -speed;
    }

    private void FixedUpdate()
    {
        if (rb.position.y < -50)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode(collision);
        //Destroy(this.gameObject);
    }

    private void Explode(Collision2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionradius);

        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();

            if (rb == null) continue;

            rb.AddExplosionForce(1000f, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), 20f);
            //Debug.Log($"{collision.gameObject.transform.position.x}, {collision.gameObject.transform.position.y}");
        }
    }
}
