using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodDoor : Door
{
    public GameObject canvas;
    public float WaterRisingSpeed = 0.02f;
    public string OpenEventName;

    override protected void activate()
    {
        base.activate();
        GameObject.Find("Water").GetComponent<WaterLevel>().risingSpeed = WaterRisingSpeed;
        InteractiveEventListener.Get().DispatchEvent(OpenEventName);
    }
}
