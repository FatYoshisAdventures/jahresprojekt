using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;

    [SerializeField] private float speed = 10f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

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

        if (Input.GetKey(KeyCode.Space))
        {
            ResetRotation();
        }
    }

    void ResetRotation()
    {
        transform.position = new Vector2(rb.position.x, rb.position.y + 1);
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;
    }
}
