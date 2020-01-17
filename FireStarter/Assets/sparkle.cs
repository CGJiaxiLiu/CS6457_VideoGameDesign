using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkle : MonoBehaviour
{
    bool sparkle_enable = true;
    public float frequency = 0.5f;
    public float max_size = 0.5f;
    public float interval = 0.9f;
    public float delay = 0.2f;
    private CanvasGroup cg;
    GameObject image_0;
    RectTransform rt_0;
    GameObject image_1;
    RectTransform rt_1;
    public Vector3 original_displacement;
    public float seed;

    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        image_0 = transform.GetChild(0).gameObject;
        rt_0 = image_0.GetComponent<RectTransform>();
        image_1 = transform.GetChild(1).gameObject;
        rt_1 = image_1.GetComponent<RectTransform>();
        seed = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(sparkle_enable)
        {
            transform.LookAt(Camera.main.transform);
            transform.position = transform.parent.position + original_displacement;

            float value = max_size * Mathf.Sin((seed + Time.time / frequency )* 2 * Mathf.PI);

            if(value / max_size < interval)
            {
                value = 0;
            }
            else
            {
                value = (value / max_size - interval) / (1 - interval) * max_size;
            }

            rt_0.sizeDelta = new Vector2(value, value);
            
            value = max_size * Mathf.Sin((seed + Time.time / frequency - delay) * 2 * Mathf.PI);

            if (value / max_size < interval)
            {
                value = 0;
            }
            else
            {
                value = (value / max_size - interval) / (1 - interval) * max_size;
            }

            rt_1.sizeDelta = new Vector2(value, value);
        }
        
    }

    public void Enable()
    {
        sparkle_enable = true;
    }

    public void Disable()
    {
        sparkle_enable = false;
        cg.alpha = 0f;
    }
}
