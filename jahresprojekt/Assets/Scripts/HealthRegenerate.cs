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
    private BoxCollider2D collider;

    [SerializeField] private bool DrawGizmo;

    void Awake()
    {
        collider = this.gameObject.GetComponent<BoxCollider2D>();
        CheckIsAreaChanged();
    }

    void CheckIsAreaChanged()
    {
        
        if (isArea == false)
        {
            collider.isTrigger = false;
        }
        else
        {
            collider.isTrigger = true;
        }
        collider.size = new Vector2(SizeX, SizeY);
    }

    private void OnDrawGizmos()
    {
        if (DrawGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(SizeX, SizeY, 0));
        
            //maybe useful
            GUIStyle style = new();
            int fontSize = 24;
            style.fontSize = Mathf.Min(Mathf.FloorToInt(Screen.width * fontSize / 1000), Mathf.FloorToInt(Screen.height * fontSize / 1000));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckIsAreaChanged();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HealthRegOnCooldown == false)
        {              
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponentInParent<Health>().RegenerateHealth(RegAmountPerRegeneration) == true)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isArea == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponentInParent<Health>().RegenerateHealth(MaxRegenerationAmount);
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
