using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    private float h = 0f;
    private float v = 0f;
    [SerializeField]
    private float speed = 10f;

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        //v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        Vector3 euler = transform.eulerAngles;

        if (euler.z > 180) euler.z = euler.z - 360;
        {
            euler.z = Mathf.Clamp(euler.z, -25, 25);
        }

        transform.eulerAngles = euler;
    }
}
