using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouDied : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Canvas canvas;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canvas.enabled = false;
    }

    void Update()
    {
        var test = rb.position.y switch
        {
            5 => false,
            0 => true,
            _ => true,
        };

        
        if (rb.position.y < -60)
        {
            canvas.enabled = true;
            canvas.GetComponent<Animator>().SetTrigger("Dead");
        }
    }
}
