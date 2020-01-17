using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassCase_level2 : Destructible
{
    public GameObject canBeBrokenBy;

    virtual public void OnCollideWithWeapon()
    {
        EventManager.TriggerEvent<BoxCollisionEvent, Vector3, float>(transform.position, 1);
        GameObject.Find("FishAI").GetComponent<WayPointController>().disableAllWayPoints = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == canBeBrokenBy)
        {
            if (other.gameObject.transform.parent.gameObject.GetComponent<Usable>().isUsing)
            {
                OnCollideWithWeapon();
                base.OnDestruct();
                Destroy(gameObject);

                GameObject.Find("Spear").GetComponent<spear>().debug_enable = true;
            }
        }
    }
}
