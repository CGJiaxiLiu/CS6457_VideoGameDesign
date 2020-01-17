using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInChest : Key
{
    public GameObject chest;
    private DualStateAnimationController chest_controller;

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        chest_controller = chest.GetComponent<DualStateAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInVolume && chest_controller.isActive && Input.GetButtonDown(pickupButton))
        {
            Pickup();
        }
    }
}
