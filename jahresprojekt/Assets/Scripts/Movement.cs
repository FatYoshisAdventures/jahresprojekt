using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float h = 0f;
    private float v = 0f;
    [SerializeField]
    private float speed = 10f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(h * speed * Time.fixedDeltaTime, rb.velocity.y);

        //Vector3 euler = transform.eulerAngles;

        //if (euler.z > 180) euler.z = euler.z - 360;
        //{
        //    euler.z = Mathf.Clamp(euler.z, -25, 25);
        //}

        //transform.eulerAngles = euler;
    }
}
