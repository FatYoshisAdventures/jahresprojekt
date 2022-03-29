using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.Netcode;

public class Shoot : NetworkBehaviour
{
    [SerializeField] GameObject[] bullets;
    [SerializeField] private Inventory inventory;
    [SerializeField] AudioClip[] audios;
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float maxvolumne = 0.1f;
    [SerializeField] private float reloadtime = 0.75f;
    [SerializeField] private GameObject mousewheeltracker;

    [HideInInspector] public Item item;
    private int index;
    private bool reloaded;
    private float shootstrenght;
    private float volumne;

    private void Awake()
    {
        reloaded = true;
    }

    void Update()
    {
        if (this.GetComponentInParent<NetworkObject>().IsOwner)
        {
            //return if on ui-element
            if (EventSystem.current.IsPointerOverGameObject()) return;
        
            //activate on left click
            //if(Input.GetMouseButton(0))
            if (Input.GetMouseButtonDown(0) && reloaded)
            {
            
                string itemname = "DEFAULT";
                if (item != null)
                {
                    itemname = item.name;
                }

                index = itemname switch
                {
                    "Rocket" => 1,
                    _ => 0,
                };

                StartCoroutine(shooting());
                StartCoroutine(reload());
                //Instantiate(bullets[index], this.transform.position, this.transform.rotation);
                shootstrenght = mousewheeltracker.GetComponent<ShootsStrength>().strength;
                volumne = Mathf.Lerp(0.25f, maxvolumne, shootstrenght / 100);
                Debug.Log(volumne);
                AudioSource.PlayClipAtPoint(audios[index], this.transform.position, volumne);
                if (item != null)
                {
                    inventory.RemoveItem(item);
                    item = null;
                }
            }
        }
    }

    IEnumerator shooting()
    {
        yield return new WaitForSeconds(delay);
        Instantiate(bullets[index], this.transform.position, this.transform.rotation);
    }

    IEnumerator reload()
    {
        reloaded = false;
        yield return new WaitForSeconds(reloadtime + delay);
        reloaded = true;
    }
}