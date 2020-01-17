using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterViewController : MonoBehaviour
{
    // Start is called before the first frame update
    public Color normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public Color waterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
    public float fogIntensity = 1.0f;
    private GameObject waterSurface;

    void Start()
    {
        // waterSurface = GameObject.FindGameObjectsWithTag("Water")[0];
    }

    // Update is called once per frame
    void Update()
    {
        //if (waterSurface.transform.position.y >= Camera.main.gameObject.transform.position.y)
        //{
        //    RenderSettings.fogColor = waterColor;
        //    RenderSettings.fogDensity = fogIntensity;
        //}
        //else
        //{
        //    RenderSettings.fogColor = normalColor;
        //    RenderSettings.fogDensity = 0.1f;
        //}
    }
}
