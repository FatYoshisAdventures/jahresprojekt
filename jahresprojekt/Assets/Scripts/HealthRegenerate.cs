using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenerate : MonoBehaviour
{
    [SerializeField] private bool isArea;
    [SerializeField] private float SizeX;
    [SerializeField] private float SizeY;
    [SerializeField] private int MaxRegenerationAmount;
    [SerializeField] private int RegAmountPerRegeneration;
    [SerializeField] private bool HasCooldown;
    [SerializeField] private float HealthRegCooldown;

    private bool HealthRegOnCooldown = false;
    
    void Awake()
    {
        CheckIsAreaChanged();
        Physics2D.IgnoreLayerCollision(11, 12);
    }

    void CheckIsAreaChanged()
    {
        Collider2D collider = this.gameObject.GetComponent<Collider2D>();
        if (isArea == false)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(SizeX, SizeY, 0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckIsAreaChanged();
        if (isArea)
        {
            if (HealthRegOnCooldown == false)
            {
                RaycastHit2D[] hits = Physics2D.BoxCastAll(this.transform.position, new Vector2(SizeX, SizeY), 0, new Vector2(0f,0f));
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        if (hit.collider.gameObject.GetComponentInParent<Health>().RegenerateHealth(RegAmountPerRegeneration) == true)
                        {
                            MaxRegenerationAmount -= RegAmountPerRegeneration;
                            if (MaxRegenerationAmount <= 0)
                            {
                                GameObject.Destroy(this.gameObject, 0);
                            }
                            else
                            {
                                if (HasCooldown == true)
                                {
                                    StartCoroutine(HealthRegCooldownRoutine());
                                }
                            }
                        }
                        else { /*player already full or error*/ }
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isArea == false)
        {
            if (gameObject.tag == "Player")
            {
                collision.collider.gameObject.GetComponentInParent<Health>().RegenerateHealth(MaxRegenerationAmount);
                GameObject.Destroy(this.gameObject, 0);
            }
        }
    }

    private IEnumerator HealthRegCooldownRoutine()
    {
        HealthRegOnCooldown = true;
        yield return new WaitForSeconds(HealthRegCooldown);
        HealthRegOnCooldown = false;
    }
}
