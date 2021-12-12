using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 0;
        Vector3 ObjectPosition = Camera.main.WorldToScreenPoint(transform.position);

        mouse.x -= ObjectPosition.x;
        mouse.y -= ObjectPosition.y;

        Vector3 target = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        target.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Debug.Log("test");
        //credit to https://www.youtube.com/watch?v=7c68z05vaX4
    }
}
