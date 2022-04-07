using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Movement : NetworkBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;

    public NetworkVariable<Vector3> Velocity = new NetworkVariable<Vector3>();

    [SerializeField] private float speed = 10f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (this.GetComponentInParent<NetworkObject>().IsOwner)
        {
            float h = Input.GetAxisRaw("Horizontal");

            MovementServerRpc(h);

            if (Input.GetKey(KeyCode.Space))
            {
                ResetPositionServerRpc();
            }


            //pretends to move player locally but gets overwritten by server saved data
            #region movement test with clamping
            if (h == 0)
            {
                //stands still, should be moving only through external influences
                rb.velocity = new Vector2(rb.velocity.x + h * speed * Time.fixedDeltaTime, rb.velocity.y);
            }
            else if (h > 0.1)
            {
                //moves to the right
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + h * speed * Time.fixedDeltaTime, 0, 2), rb.velocity.y);
            }
            else if (h < -0.1)
            {
                //moves to the left
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + h * speed * Time.fixedDeltaTime, -2, 0), rb.velocity.y);
            }
            #endregion
            //rb.AddForce(new Vector2(h * speed * Time.fixedDeltaTime,0), ForceMode2D.Impulse);
            //rb.velocity = new Vector2(rb.velocity.x + h * speed * Time.fixedDeltaTime, rb.velocity.y);

        }
        //rb.velocity = Velocity.Value;
        transform.position = Velocity.Value;
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    void MovementServerRpc(float horizontal)
    {
        if (horizontal == 0)
        {
            //stands still, should be moving only through external influences
            rb.velocity = new Vector2(rb.velocity.x + horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (horizontal > 0.1)
        {
            //moves to the right
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + horizontal * speed * Time.fixedDeltaTime, 0, 2), rb.velocity.y);
        }
        else if (horizontal < -0.1)
        {
            //moves to the left
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + horizontal * speed * Time.fixedDeltaTime, -2, 0), rb.velocity.y);
        }
        //Velocity.Value = rb.velocity;
        Velocity.Value = this.transform.position;
    }
    
    [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
    void ResetPositionServerRpc()
    {
        rb.angularVelocity = 0f;
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position = new Vector2(rb.position.x, rb.position.y + 1);
        rb.velocity = Vector2.zero;
        ResetPositionClientRpc();
    }

    [ClientRpc]
    void ResetPositionClientRpc()
    {
        rb.angularVelocity = 0f;
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position = new Vector2(rb.position.x, rb.position.y + 1);
        rb.velocity = Vector2.zero;
    }

    void ResetRotation()
    {
        transform.position = new Vector2(rb.position.x, rb.position.y + 1);
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;
    }
}
