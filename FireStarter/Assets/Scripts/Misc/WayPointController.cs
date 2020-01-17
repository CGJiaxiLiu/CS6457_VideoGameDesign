using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour
{
    private GameObject water;
    private GameObject[] waypoints;
    public bool disableAllWayPoints;

    void Start()
    {
        water = GameObject.Find("Water");
        waypoints = new GameObject[gameObject.transform.GetChild(0).childCount];

        for(int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
        {
            waypoints[i] = gameObject.transform.GetChild(0).GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (!disableAllWayPoints)
        {
            foreach (GameObject temp in waypoints)
            {
                if (temp.transform.position.y >= water.transform.position.y)
                {
                    temp.tag = "Untagged";
                }
                else
                {
                    temp.tag = "waypoint";
                }
            }
        }
        else 
        {
            foreach (GameObject temp in waypoints)
            {
                if(temp.name != "1" && temp.name != "0")
                {
                    temp.tag = "Untagged";
                }
            }
        }
    }
}
