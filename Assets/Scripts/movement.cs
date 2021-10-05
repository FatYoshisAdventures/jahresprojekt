using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
    }

    void FixedUpdate()
    {
        //rb2d.AddForce(new Vector2(h * speed, v * speed), ForceMode2D.Impulse);
        //rb2d.MovePosition(new Vector2(rb2d.position.x + h * speed, rb2d.position.y + v * speed));
        transform.position += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        //rb2d.velocity = new Vector2(h * speed, v * speed);
    }
}
