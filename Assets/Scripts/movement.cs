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
        //v = Input.GetAxis("Vertical");
        transform.position += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.transform.localScale.y/2, gameObject.transform.position.z), .2f);
    }
}
