using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotating : MonoBehaviour
{
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(Time.time * speed, new Vector3(1, 0, 0));
        transform.rotation = Quaternion.Euler(Time.time * speed, 0, 0);
    }
}
