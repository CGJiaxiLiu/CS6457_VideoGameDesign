using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveEventListener : MonoBehaviour
{
    static InteractiveEventListener _instance;

    public static InteractiveEventListener Get()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    public virtual void DispatchEvent(string eventName)
    {

    }
}
