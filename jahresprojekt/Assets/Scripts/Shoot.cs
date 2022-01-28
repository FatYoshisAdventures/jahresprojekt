using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    void Update()
    {
        //return if on ui-element
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        //activate on left click

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, this.transform.position, this.transform.rotation);
        }
    }
}
