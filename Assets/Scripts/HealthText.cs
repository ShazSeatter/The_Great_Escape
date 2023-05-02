using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    // pixels per second 
    public Vector3 moveSpeed = new Vector3(0,75, 0);
    public float timeToFade = 1f;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    private float timeElapsed =0f;

    private Color startColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        // starting value for color 
        startColor = textMeshPro.color;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // rect transform because this is UI element
        //
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if(timeElapsed < timeToFade)
        {
            // then this is the updated value
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        } else
        {
            // get rid of it entirely 
            Destroy(gameObject);
        }
    }
}
