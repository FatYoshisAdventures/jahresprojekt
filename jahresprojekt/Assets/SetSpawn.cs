using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawn : MonoBehaviour
{
    [SerializeField] private bool WorldSpawn;
    [SerializeField] private bool SpikeSpawn;
          
    [SerializeField] private float RangeX = 1;
    [SerializeField] private float RangeY = 1;
    [SerializeField] private float OffsetX;
    [SerializeField] private float OffsetY;

    [SerializeField] private bool DrawGizmo;

    private BoxCollider2D triggerCollider;

    // Start is called before the first frame update
    void Awake()
    {
        this.triggerCollider = GetComponent<BoxCollider2D>();
        CheckSpikeSpawn();
    }

    private void OnDrawGizmos()
    {
        if (DrawGizmo && Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(triggerCollider.transform.position + (Vector3)triggerCollider.offset, new Vector3(Mathf.Abs(triggerCollider.size.x), Mathf.Abs(triggerCollider.size.y), 0));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check in Certain Radius if player is there if yes set spawn
        if (WorldSpawn)
        {
            //action performed
            //maybe do as event?
            ActiveSpawns.WorldSpawn = new Vector3(0, 0, 0);
        }
        CheckSpikeSpawn();
    }

    void CheckSpikeSpawn()
    {
        if (SpikeSpawn == false)
        {
            triggerCollider.enabled = false;
        }
        else
        {
            triggerCollider.enabled = true;
            triggerCollider.size = new Vector2(RangeX, RangeY);
            triggerCollider.offset = new Vector2(OffsetX, OffsetY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sets active SpikeSpawn to this
        ActiveSpawns.SpikeSpawn = this.transform.position;
    }
}
