using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : Interactable
{
    //protected Interactable interactiveBaseComp;
    public string pickupButton = "PickUp";
    public string targetSlot = "inventory_slot";
    public string handSlot = "Right_hand_slot";
    public Sprite item_icon;
    public float maxRecoverySpeed = 1.0f;
    inventory inventoryManager;
    public bool generateSound = true;
    GameObject sparkle_object;
    public bool GenerateDropEvent = false;
    public string DropEventName;
    public bool canDrop = true;
    private AudioSource pickUpSound;
    private AudioSource dropSound;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoyUI").GetComponent<inventory>();
        GetComponent<Rigidbody>().maxDepenetrationVelocity = maxRecoverySpeed;
        sparkle_object = GameObject.Instantiate(inventoryManager.sparkle_prefab, transform);
        //sparkle_object.transform.position = transform.position + GetComponent<InGameBubble>().relativePosition;
        sparkle_object.transform.localScale = new Vector3(1.0f / transform.localScale.x, 1.0f / transform.localScale.y, 1.0f / transform.localScale.z);
        
    }

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        pickUpSound = gameObject.AddComponent<AudioSource>();
        dropSound = gameObject.AddComponent<AudioSource>();

        pickUpSound.clip = Misc.Get().pickup;
        dropSound.clip = Misc.Get().drop;
        sparkle_object.GetComponent<sparkle>().original_displacement = GetComponent<InGameBubble>().relativePosition;
    }

    // Update is called once per frame
    override public void Update()
    {
        base.Update();

        if (playerInVolume)
        {
            if (Input.GetButtonDown(pickupButton) && canInteract)
            {
                Pickup();
                base.OnAfterInteract();
            }
        }
    }

    virtual protected void Pickup(bool random = false)
    {
        if (player && canInteract)
        {
            GameObject slot = GameObject.Find(targetSlot);
            if (slot)
            {
                if(!inventoryManager.addItem(this))
                {
                    return;
                }

                if(base.useable && base.useInstruction != "")
                {
                    InstructionUIManager.Get().SetActive(UI_Type.RightClick, base.useInstruction);
                } 

                if (canDrop)
                {
                    InstructionUIManager.Get().SetActive(UI_Type.Keyboard, "Drop");
                }

                float pos_range = 0.1f;
                gameObject.transform.SetParent(slot.transform);
                if(random)
                {
                    gameObject.transform.position = slot.transform.position + new Vector3(Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range));
                }
                else
                {
                    gameObject.transform.position = slot.transform.position;
                }

                gameObject.transform.rotation = slot.transform.rotation;

                //if (generateSound)
                //{
                //    EventManager.TriggerEvent<BombBounceEvent, Vector3>(transform.position);
                //}

                foreach (Collider one in GetComponents<Collider>())
                {
                    one.enabled = false;
                }

                if (GetComponent<Rigidbody>())
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                }
                pickUpSound.Play();
                canInteract = false;
            }

            
        }

        if (GetComponent<InGameBubble>())
        {
            GetComponent<InGameBubble>().UI_Enabled = false;
        }

        sparkle_object.GetComponent<sparkle>().Disable();
    }

    public void dropItem()
    {
        gameObject.transform.SetParent(null);

        foreach (Collider one in GetComponents<Collider>())
        {
            one.enabled = true;
        }

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        dropSound.Play();
        canInteract = true;

        if(GenerateDropEvent)
        {
            InteractiveEventListener.Get().DispatchEvent(DropEventName);
        }
    }

    public void moveBack()
    {
        GameObject slot = GameObject.Find(targetSlot);
        if (slot)
        {
            float pos_range = 0.1f;
            gameObject.transform.SetParent(slot.transform);
            gameObject.transform.position = slot.transform.position + new Vector3(Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range));
            gameObject.transform.rotation = slot.transform.rotation;
            print("find back slot!");
        }
    }

    public void moveToHand()
    {
        GameObject slot = GameObject.Find(handSlot);
        if (slot)
        {
            gameObject.transform.SetParent(slot.transform);
            gameObject.transform.position = slot.transform.position;
            gameObject.transform.rotation = slot.transform.rotation;

            print("find slot!");
        }
    }

    public void Consume()
    {
        inventoryManager.deleteItem(this);
        Destroy(gameObject);
    }
}
