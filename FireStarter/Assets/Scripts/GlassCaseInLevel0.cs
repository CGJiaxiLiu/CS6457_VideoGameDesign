using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCaseInLevel0 : MonoBehaviour
{
    public GameObject canBeBrokenBy;
    public GameObject KeyInside;
    public GameObject tutBubble;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Collider c in KeyInside.GetComponents<Collider>())
        {
            c.enabled = false;
        }
        KeyInside.GetComponent<Rigidbody>().isKinematic = true;
        KeyInside.GetComponent<Floatable>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == canBeBrokenBy)
        {
            if (other.gameObject.transform.parent.gameObject.GetComponent<Usable>().isUsing)
            {
                foreach (Collider c in KeyInside.GetComponents<Collider>())
                {
                    c.enabled = true;
                }
                EventManager.TriggerEvent<BoxCollisionEvent, Vector3, float>(transform.position, 1);
                KeyInside.GetComponent<Rigidbody>().isKinematic = false;
                KeyInside.GetComponent<Floatable>().enabled = true;
                Destroy(gameObject);
                Destroy(tutBubble);
            }
        }
    }
}
