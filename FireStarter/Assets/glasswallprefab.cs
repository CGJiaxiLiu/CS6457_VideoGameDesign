using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glasswallprefab : MonoBehaviour
{
    public Material glass;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            if(obj.GetComponent<Rigidbody>())
            {
                obj.GetComponent<Rigidbody>().maxDepenetrationVelocity = Random.Range(0.2f, 3);
            }
            obj.GetComponent<Renderer>().material = glass;

        }
        Invoke("delayDisable", 3);
    }

    void delayDisable()
    {
        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            obj.GetComponent<Collider>().enabled = false;
            Destroy(obj.GetComponent<Rigidbody>());
        }
    }
}
