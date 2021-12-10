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
    private float explosionradius = 1f;

    [SerializeField]
    private float explosionforce = 100f;

    [SerializeField]
    public GameObject player;

    private bool hit = false;
    private float delay = 2;

    private void Start()
    {
        rb.velocity = transform.right * speed;
        player = GameObject.Find("player");
        

        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), player.GetComponentInChildren<PolygonCollider2D>());
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
        hit = true;
        
        if (collision.gameObject.tag == "Player") return;

        if (collision.gameObject.tag == "ground")
        {
            delay = 0.3f;
        }
        else
        {
            Explode(collision);
        }
    }

    private void Explode(Collision2D collision)
    {
        

        //todo
        //spawn explosion point between the two colliding objects
        //with this both objects can be exploded away instead of only one
        Vector3 pointbetween = (collision.gameObject.transform.position + this.gameObject.transform.position) / 2;
        //Instantiate(explosionpoint, pointbetween, Quaternion.Euler(0, 0, 0));

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

            //Debug.Log($"{collision.gameObject.transform.position.x}, {collision.gameObject.transform.position.y}");
        }
    }
}
