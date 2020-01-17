using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class postprocessmanager : MonoBehaviour
{
    static postprocessmanager __instance;

    static public postprocessmanager Get()
    {
        return __instance;
    }

    PostProcessVolume[] posts;

    // Start is called before the first frame update
    void Start()
    {
        __instance = this;
        posts = GetComponents<PostProcessVolume>();
        Init();
    }

    public void enableBlur()
    {
        posts[1].weight = 1f;
    }

    public void disableBlur()
    {
        posts[1].weight = 0f;
    }

    public void enableVegnette()
    {
        posts[2].weight = 1f;
    }

    public void disableVegnette()
    {
        posts[2].weight = 0f;
    }

    public void Init()
    {
        disableBlur();
        disableVegnette();
    }
}
