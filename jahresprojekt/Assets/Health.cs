using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            GameObject.Destroy(this.gameObject, 0);
            //Instatniate corpse
            if (gameObject.tag.Equals("Player"))
            {
                //Finds animator on player death to kill player
                FindObjectOfType<Canvas>().GetComponent<Animator>().SetTrigger("Dead");
            }
        }
    }
}
