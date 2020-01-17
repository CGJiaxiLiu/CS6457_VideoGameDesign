using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestFragment : MonoBehaviour
{
    public Material glass;
    public Material frame;

    void Start()
    {
        foreach(Transform child in transform)
        {
            GameObject obj = child.gameObject;
            obj.GetComponent<Rigidbody>().maxDepenetrationVelocity = Random.Range(0.2f, 3);
            if (obj.name.StartsWith("Frame"))
            {
                obj.GetComponent<Renderer>().material = frame;
            }
            else
            {
                obj.GetComponent<Renderer>().material = glass;
            }
        }
        Invoke("delayDisable", 3);
        Invoke("delayDestroy", 6);

    }

    void delayDisable()
    {
        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            obj.GetComponent<Collider>().enabled = false;
            obj.GetComponent<Rigidbody>().detectCollisions = false;
        }
    }

    void delayDestroy()
    {
        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            Destroy(obj);
        }
    }
}
