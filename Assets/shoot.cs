using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //activate on left click
        if (Input.GetMouseButtonDown(0))
        {

            Instantiate(bullet, this.transform.position, this.transform.rotation);
            Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(0, 500));
        }
    }
}
