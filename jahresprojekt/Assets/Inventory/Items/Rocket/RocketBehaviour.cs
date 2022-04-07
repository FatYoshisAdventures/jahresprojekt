using System;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    
    private float delay = 2;
    
    [SerializeField] float speed = 5f;

    [SerializeField] float explosionradius = 1f;

    [SerializeField] float explosionforce = 100f;

    [SerializeField] GameObject player;

    [SerializeField] GameObject explosion;

    List<GameObject> collidedWith = new List<GameObject>();

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        //Set initial speed
        rb.velocity = transform.right * speed;

        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), player.GetComponentInChildren<PolygonCollider2D>());
    }

    void FixedUpdate()
    {
        Move();

        DestroyBelowLevel();
    }

    private void Move()
    {
        if (rb.velocity.x > 0.2 || rb.velocity.y > 0.2 || rb.velocity.x < -0.2 || rb.velocity.y < -0.2)
        {
            Vector3 diff = (transform.position + (Vector3)rb.velocity) - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
    }

    private void DestroyBelowLevel()
    {
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

        Explode(collision);
    }

    void DoDamage(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health gameObject))
        {
            gameObject.DoDamageServerRpc(1);
        }
    }

    void Explode(Collision2D collision)
    {
        //Damage colliding Object
        DoDamage(collision);
        
        //Summon explosion effect
        GameObject tempExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(tempExplosion, 3f);
        
        //Get nearby objects affected by explosion
        Vector3 pointbetween = (collision.gameObject.transform.position + this.gameObject.transform.position) / 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pointbetween, explosionradius);

        //Add explosion force to nearby objects
        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.TryGetComponent(out Rigidbody2D rb))
            {
                rb.AddExplosionForce(explosionforce, pointbetween, explosionradius);
            }
        }
        
        //Destroy rocket
        Destroy(this.gameObject);
    }
}
