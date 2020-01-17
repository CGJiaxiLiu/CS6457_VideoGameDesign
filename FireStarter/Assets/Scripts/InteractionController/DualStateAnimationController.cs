using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DualStateAnimationController : Interactable
{
    public string activateTriggerCommnand = "activate";
    public string deactivateTriggerCommnand = "deactivate";
    public bool autoDeactivate = false;
    public bool cannotDeactive = false;
    public float autoDeactivateTime = 5.0f;
    private float lastOpenTime = 0.0f;
    public string interactiveButton = "Interact";
    public bool isActive
    {
        get;
        private set;
    }

    public bool isControllerEnabled = true;

    public bool enableBubbleOnActivate = false;
    public bool disableBubbleOnActivate = false;

    private Animator animatior;

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        animatior = GetComponent<Animator>();
    }

    // Update is called once per frame
    override public void Update()
    {
        if(isControllerEnabled)
        {
            if (playerInVolume)
            {
                if (Input.GetButtonDown(interactiveButton))
                    //if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (!isActive)
                    {
                        activate();
                    }

                    else if(!cannotDeactive)
                    {
                        deactivate();
                    }
                }
            }

            if(!cannotDeactive && autoDeactivate)
            {
                if(isActive && (Time.time - lastOpenTime) > autoDeactivateTime)
                {
                    deactivate();
                }
            }
        }

        base.Update();
    }

    virtual protected void activate()
    {
        if(canActivate())
        {
            animatior.SetTrigger(activateTriggerCommnand);
            Invoke("makeActive", 0.1f);
            lastOpenTime = Time.time;
            if (enableBubbleOnActivate || disableBubbleOnActivate)
            {
                InGameBubble bubbleComp = GetComponent<InGameBubble>();
                if (enableBubbleOnActivate)
                {
                    bubbleComp.enabled = true;
                }
                if (disableBubbleOnActivate)
                {
                    bubbleComp.UI_Enabled = false;
                }
            }
        }
    }

    virtual protected void deactivate()
    {
        if(canDeactivate())
        {
            animatior.SetTrigger(deactivateTriggerCommnand);
            isActive = false;
        }
    }

    virtual public bool canActivate()
    {
        return true;
    }

    virtual public bool canDeactivate()
    {
        return true;
    }

    void makeActive()
    {
        isActive = true;
    }
}
