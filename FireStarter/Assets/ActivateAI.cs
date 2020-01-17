using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAI : MonoBehaviour
{
    public bool initialPlayerInVolume = true;
    static bool playerInVolume = true;

    static public bool Get()
    {
        return playerInVolume;
    }
    //GameObject player;
    //BoxCollider bc;
    //bool last_frame_in;

    // Start is called before the first frame update
    void Start()
    {
        playerInVolume = initialPlayerInVolume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("player enter");
            playerInVolume = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("player exit");
            playerInVolume = false;
        }
    }
}
