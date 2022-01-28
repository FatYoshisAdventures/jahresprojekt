using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootsStrength : MonoBehaviour
{
    public float strength = 50f;

    [SerializeField] float sensitivity = 10f;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !Input.GetKey(KeyCode.LeftControl))
        {
            strength += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            strength = Mathf.Clamp(strength, 1, 100);
        }
    }
}