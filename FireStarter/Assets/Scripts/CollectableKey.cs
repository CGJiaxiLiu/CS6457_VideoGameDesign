using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        if(anim == null)
        {
            Debug.Log("Animator could not be found.");
        }
        anim.SetFloat("Multiplier", 0.0f);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            KeyCollector kc = c.attachedRigidbody.gameObject.GetComponent<KeyCollector>();
            if (kc != null)
            {
                kc.ReceiveKey();
                anim.SetFloat("Multiplier", 1.0f);
            }
        }
    }
    private void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            KeyCollector kc = c.attachedRigidbody.gameObject.GetComponent<KeyCollector>();
            if (kc != null)
            {
                anim.SetFloat("Multiplier", 0.0f);
            }
        }
    }
}
