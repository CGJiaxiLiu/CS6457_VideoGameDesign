using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollisionReporter : MonoBehaviour
{

    void OnCollisionEnter(Collision c)
    {

        if (c.impulse.magnitude > 0.25f)
        {
            //we'll just use the first contact point for simplicity
            EventManager.TriggerEvent<BoxCollisionEvent, Vector3, float>(c.contacts[0].point, c.impulse.magnitude);
        }
            

//				foreach (ContactPoint contact in c.contacts) {
//
//						if (c.impulse.magnitude > 0.5f)
//								EventManager.TriggerEvent<AudioEventManager.BoxAudioEvent, Vector3> (contact.point);
//						
//				}
    }
}
