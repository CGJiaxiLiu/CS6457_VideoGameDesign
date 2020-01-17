using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingTrigger : MonoBehaviour
{
    public string FirstEnterEventname;
    public string SecondEnterEventname;

    virtual protected void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if(GameObject.Find("crowbar").transform.parent == null)
            {
                InteractiveEventListener.Get().DispatchEvent(FirstEnterEventname);
            }
            else
            {
                InteractiveEventListener.Get().DispatchEvent(SecondEnterEventname);
            }
        }
    }
}
