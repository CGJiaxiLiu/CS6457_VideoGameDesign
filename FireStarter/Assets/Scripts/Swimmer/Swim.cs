using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Swim : MonoBehaviour
{
    private GameObject water;
    public Color normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public Color waterColor = new Color(0.1724368f, 0.1934705f, 0.2924528f, 1f);
    public float density = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // RenderSettings.fog = true;
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = density;

        water = GameObject.FindGameObjectsWithTag("Water")[0];
    }

    bool IsUnderwater()
    {
        return gameObject.transform.position.y - 0.1 < water.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.fog = IsUnderwater();

        if (IsUnderwater())
        {
            postprocessmanager.Get().enableVegnette();
        }
        else
        {
            postprocessmanager.Get().disableVegnette();
        }
    }
}
