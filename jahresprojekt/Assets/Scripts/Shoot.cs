using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    void Update()
    {
        //activate on left click

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, this.transform.position, this.transform.rotation);
        }
    }
}
