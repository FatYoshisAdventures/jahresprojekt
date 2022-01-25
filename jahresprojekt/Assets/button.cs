using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class button : MonoBehaviour
{
    [SerializeField] private UnityEvent onButtonPressed;
    // Start is called before the first frame update
    void Start()
    {
        if (onButtonPressed == null)
        {
            onButtonPressed = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            onButtonPressed.Invoke();
        }
    }
}
