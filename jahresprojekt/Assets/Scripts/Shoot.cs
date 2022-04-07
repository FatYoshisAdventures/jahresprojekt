using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.Netcode;

public class Shoot : NetworkBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ActiveItem activeItem;

    [SerializeField] AudioClip[] audios;
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float maxVolume = 0.1f;
    [SerializeField] private float reloadtime = 0.75f;
    [SerializeField] private float volume = 0.5f;

    private int index;
    private bool reloaded;

    private void Awake()
    {
        reloaded = true;
    }

    void Update()
    {
        //return if not owner
        if (!this.GetComponentInParent<NetworkObject>().IsOwner) return;

        //return if on ui-element
        if (EventSystem.current.IsPointerOverGameObject()) return;

        //return if no item is selected
        if (activeItem.item == null) return;

        //activate on left click
        //if(Input.GetMouseButton(0))
        if (Input.GetMouseButtonDown(0) && reloaded)
        {
            string itemname = activeItem.item != null ? activeItem.item.name : "DEFAULT";

            index = itemname switch
            {
                "Rocket" => 1,
                _ => 0,
            };

            StartCoroutine(shooting());
            StartCoroutine(reload());


            shootstrenght = mousewheeltracker.GetComponent<ShootsStrength>().strength;
            volumne = Mathf.Lerp(0.25f, maxvolumne, shootstrenght / 100);
                
            //AudioSource.PlayClipAtPoint(audios[index], this.transform.position, volumne);
            AudioSource.PlayClipAtPoint(audios[index], this.transform.position, volume);
            Debug.Log($"Volume: {volume}");
        }
    }

    IEnumerator shooting()
    {
        yield return new WaitForSeconds(delay);
        activeItem.item.Use(this.transform);
    }

    IEnumerator reload()
    {
        reloaded = false;
        yield return new WaitForSeconds(reloadtime + delay);
        reloaded = true;
    }
}