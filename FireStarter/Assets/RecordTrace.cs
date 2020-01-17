using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTrace : MonoBehaviour
{
    private ArrayList positionList;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RecordPosition", 2.0f, 3f);
        positionList = new ArrayList();
    }

    void RecordPosition()
    {
        positionList.Add(gameObject.transform.position);
    }
}
