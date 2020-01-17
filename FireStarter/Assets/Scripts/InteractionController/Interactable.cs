using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public string pickUpInstruction;
    public string useInstruction;

    public bool useable = false;
    public bool reuseable = true;

    private bool afterUse = false;
     
    protected Collider activeVolume = null;

    protected bool playerInVolume = false;
    protected bool canInteract = true;

    protected GameObject player;

    public bool GenerateEnterVolumeEvent = false;
    public string EnterVolumeEventName;
    public bool GenerateInteractEvent = false;
    public string InteractEventName;

    virtual public void Update()
    {
        //print(gameObject.name + "instruction:" + instruction);
    }

    virtual public void Start()
    {
        Collider[] collider_array = GetComponents<Collider>();

        foreach (Collider one in collider_array)
        {
            if (one.isTrigger)
            {
                activeVolume = one;
                break;
            }
        }
    }

    virtual protected void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player") && canInteract)
        {
            if (GenerateEnterVolumeEvent)
            {
                InteractiveEventListener.Get().DispatchEvent(EnterVolumeEventName);
            }

            playerInVolume = true;
            player = c.gameObject;

            if(!afterUse || reuseable)
            {
                InstructionUIManager.Get().SetActive(UI_Type.LeftClick, pickUpInstruction);
            }
        }
    }

    virtual protected void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player") && canInteract)
        {
            playerInVolume = false;
            player = null;

            if (!afterUse || reuseable)
            {
                InstructionUIManager.Get().Deactive(UI_Type.LeftClick);
            }
        }
    }

    public void OnAfterInteract()
    {
        if(!afterUse || reuseable)
        {
            InstructionUIManager.Get().Deactive(UI_Type.LeftClick);
            afterUse = true;
        }

        if (GenerateInteractEvent)
        {
            InteractiveEventListener.Get().DispatchEvent(InteractEventName);
            print("After use");
        }
    }
}
