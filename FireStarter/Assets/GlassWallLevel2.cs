using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassWallLevel2 : MonoBehaviour
{
    public Material glass;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.childCount == 0)
            {
                SetMaterial(child);
            }
            else
            {
                foreach (Transform grandChild in child)
                {
                    SetMaterial(grandChild);
                }
            }
        }

        Invoke("delayDestroy", 3);
    }

    void SetMaterial(Transform trans)
    {
        GameObject obj = trans.gameObject;
        if(obj.GetComponent<Renderer>() != null)
        {
            obj.GetComponent<Renderer>().material = glass;
        }
    }

    void delayDestroy()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount != 0)
            {
                foreach (Transform grandChild in child)
                {
                    GameObject obj = grandChild.gameObject;
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
