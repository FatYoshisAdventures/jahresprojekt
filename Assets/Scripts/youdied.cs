using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class youdied : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Animator death;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }   


    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < -60)
        {
            death.SetTrigger("Dead");
        }
    }
}
