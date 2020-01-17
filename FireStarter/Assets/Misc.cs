using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misc : MonoBehaviour
{
    public GameObject anchor_prefab;
    public AudioClip doorOpen;
    public AudioClip pickup;
    public AudioClip drop;
    public AudioClip dive;
    public AudioClip swim;

    static Misc __instance;

    private void Awake()
    {
        __instance = this;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public static Misc Get()
    {
        return __instance;
    }
}
