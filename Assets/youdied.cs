using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class youdied : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Canvas canvas;
    private Rigidbody2D rb;

    private bool dead = false;

    private bool mFaded = true;
    public float duration = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fade()
    {
        var canvasGroup = canvas.GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, mFaded ? 1 : 0)); 

        mFaded = !mFaded;
    }

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < -60)
        {
            if (!dead)
            {
                Fade();
            }
            
            dead = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(10, 2), ForceMode2D.Impulse);
        }
    }
}
