using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed = 5f;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] float explosionradius = 1f;

    [SerializeField] float explosionforce = 100f;

    public GameObject player;

    private float delay = 2;

    List<GameObject> collidedWith = new List<GameObject>();

    void Start()
    {
        float strenght = GameObject.Find("mousewheeltracker").GetComponent<ShootsStrength>().strength;
        rb.velocity = transform.right * speed * (strenght / 100);
        player = GameObject.FindGameObjectWithTag("Player");


        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), player.GetComponentInChildren<PolygonCollider2D>());
    }

    void FixedUpdate()
    {
        if (rb.velocity.x > 0.2 || rb.velocity.y > 0.2 || rb.velocity.x < -0.2 || rb.velocity.y < -0.2)
        {
            Vector3 diff = (transform.position + (Vector3)rb.velocity) - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
            
        
        if (rb.position.y < -50)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") return;

        if (collidedWith.Contains(collision.gameObject)) return;

        collidedWith.Add(collision.gameObject);

        if (collision.gameObject.tag == "ground")
        {
            Destroy(this.gameObject, 1f);
        }
        else
        {
            DoDamage(collision);
            Explode(collision);
        }
    }

    void DoDamage(Collision2D collision)
    {
        try
        {
            collision.gameObject.GetComponent<Health>().DoDamageServerRpc(1);
        }
        catch (Exception) { } //no health object found on hit object
        Destroy(this.gameObject, delay);
    }

    void Explode(Collision2D collision)
    {
        Vector3 pointbetween = (collision.gameObject.transform.position + this.gameObject.transform.position) / 2;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(pointbetween, explosionradius);

        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.gameObject.GetComponent<Rigidbody2D>();

            if (rb == null)
            {

            }
            else
            {
                rb.AddExplosionForce(explosionforce * rb.velocity.magnitude, pointbetween, explosionradius * rb.velocity.magnitude);
            }
        }
    }
}