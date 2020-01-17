using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public float risingSpeed = 0.01f;
    public float maxHeight = 7f;
    private float initHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        initHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < maxHeight && risingSpeed > 0) gameObject.transform.position = gameObject.transform.position + new Vector3(0, risingSpeed * Time.deltaTime, 0);
    }
}
