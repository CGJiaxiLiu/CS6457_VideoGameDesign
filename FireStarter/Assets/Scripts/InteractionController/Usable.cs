using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour
{
    public bool continuously = false;
    public bool GenerateUseEvent = false;
    public string UseEventName;

    public bool isUsing
    {
        protected set;
        get;
    } = false;

    virtual public void Use()
    {
        if(GenerateUseEvent)
        {
            InteractiveEventListener.Get().DispatchEvent(UseEventName);
        }
    }
}
