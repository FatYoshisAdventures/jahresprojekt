using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RocketBehaviour : NetworkBehaviour
{
    Rigidbody2D rb;
    
    private float delay = 2;
    
    [SerializeField] float speed = 5f;

    [SerializeField] float explosionradius = 1f;

    [SerializeField] float explosionforce = 100f;

    [SerializeField] GameObject player;

    [SerializeField] GameObject explosion;
    
    [SerializeField] AudioClip sound;

    [SerializeField] float volume = 0.75f;

    List<GameObject> collidedWith = new List<GameObject>();

    public NetworkVariable<Vector3> velocity = new NetworkVariable<Vector3>();


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();

        //rb.velocity = transform.right * speed;

        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), player.GetComponentInChildren<PolygonCollider2D>());

        AudioSource.PlayClipAtPoint(sound, this.transform.position, volume);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        getVelocityServerRpc();

        Move();

        DestroyBelowLevel();
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    private void getVelocityServerRpc()
    {
        velocity.Value = rb.velocity;
    }

    
    public void SetSpeed(Vector3 destination)
    {
        Vector3 difference = destination - this.transform.position;

        rb.velocity = difference * speed;
    }


    private void Move()
    {
        rb.velocity = velocity.Value;
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
        if (collision.gameObject.tag == "Player" && (collision.gameObject.GetComponentInParent<NetworkBehaviour>().OwnerClientId == gameObject.GetComponent<NetworkBehaviour>().OwnerClientId)) return;

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
        
        //only server should execute those functions, if the velocity of bullets near this one changes the pos over the networkvarable attached
        if (IsServer)
        {
            //Damage colliding Object
            DoDamage(collision);

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
        }

        SpawnExplosionServerRpc();

        if (IsServer)
        {
            //Destroy rocket
            this.GetComponent<NetworkObject>().Despawn();
        }
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    void SpawnExplosionServerRpc()
    {
        //Summon explosion effect
        GameObject tempExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        tempExplosion.GetComponent<NetworkObject>().Spawn();
        Destroy(tempExplosion, 3f);
    }
}
