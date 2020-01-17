using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    WaterLevel m_waterLevel;

    public float next_waterLevel_speed = 2;
    public float next_waterLevel_height = 40;
    public Door door0;
    // Start is called before the first frame update
    void Start()
    {
        m_waterLevel = GameObject.FindWithTag("Water").GetComponent<WaterLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!door0.isLocked) {
            m_waterLevel.maxHeight = next_waterLevel_height;
            m_waterLevel.risingSpeed = next_waterLevel_speed;   
        }
    }
}
