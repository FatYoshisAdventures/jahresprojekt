using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    [SerializeField] private float speed = 10f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(h * speed * Time.fixedDeltaTime, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space))
        {
            ResetRotation();
        }
    }

    private void ResetRotation()
    {
        transform.position = new Vector2(rb.position.x, rb.position.y + 1);
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;
    }
}
