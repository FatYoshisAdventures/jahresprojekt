using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float explosionradius = 1f;

    [SerializeField]
    private float explosionforce = 100f;

    private bool hit = false;
    private float delay = 2;

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
        if (hit)
        {
            Destroy(this.gameObject, delay);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode(collision);
        hit = true;
        if (collision.gameObject.tag == "ground")
        {
            delay = 0.3f;
        }
    }

    private void Explode(Collision2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionradius);

        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();

            if (rb == null) continue;

            rb.AddExplosionForce(explosionforce * rb.velocity.magnitude, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), explosionradius * rb.velocity.magnitude);
            //Debug.Log($"{collision.gameObject.transform.position.x}, {collision.gameObject.transform.position.y}");
        }
    }
}
