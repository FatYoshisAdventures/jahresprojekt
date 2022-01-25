using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandableBridge : MonoBehaviour
{
    private bool active = false;
    [SerializeField] private float rate = 0.05f;
    [SerializeField] private Transform endpoint;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(this.gameObject.layer, 11);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(1f, 0f) * rate;
            if (gameObject.transform.position.x >= 5)
            {
                active = false;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    public void onToggleActive()
    {
        active = !active;
    }
}
