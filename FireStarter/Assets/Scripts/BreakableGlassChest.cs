using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGlassChest : Destructible
{
    public GameObject canBeBrokenBy;
    public GameObject KeyInside;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Collider c in KeyInside.GetComponents<Collider>())
        {
            c.enabled = false;
        }
        KeyInside.GetComponent<Rigidbody>().isKinematic = true;
        if (KeyInside.GetComponent<Floatable>() != null)
        {
            KeyInside.GetComponent<Floatable>().enabled = false;
        }
    }

    virtual public void OnCollideWithWeapon()
    {
        foreach (Collider c in KeyInside.GetComponents<Collider>())
        {
            c.enabled = true;
        }
        EventManager.TriggerEvent<BoxCollisionEvent, Vector3, float>(transform.position, 1);
        KeyInside.GetComponent<Rigidbody>().isKinematic = false;
        if(KeyInside.GetComponent<Floatable>() != null)
        {
            KeyInside.GetComponent<Floatable>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == canBeBrokenBy)
        {
            if (other.gameObject.transform.parent.gameObject.GetComponent<Usable>().isUsing)
            {
                OnCollideWithWeapon();
                base.OnDestruct();
            }
        }
    }
}
