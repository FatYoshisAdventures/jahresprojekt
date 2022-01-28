using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhealth = 3;

    [SerializeField] int _Health = 0;

    [HideInInspector]
    public int health 
    {
        get { return _Health; }
        set {
            if (value <= 0)
            {
                GameObject.Destroy(this.gameObject, 0);
                //Instatniate corpse
                if (gameObject.tag.Equals("Player"))
                {
                    //Finds animator on player death to kill player
                    FindObjectOfType<Canvas>().GetComponent<Animator>().SetTrigger("Dead");
                }
            }
            else
            {
                //sets health value between 0 and the maxamount of health
                _Health = Mathf.Clamp(value, 0, maxhealth);
            }
        } 

    }

    void Start()
    {
        health = maxhealth;
    }
}
