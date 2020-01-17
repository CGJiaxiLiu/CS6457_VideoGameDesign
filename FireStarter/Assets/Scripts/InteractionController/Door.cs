using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : DualStateAnimationController
{
    // Start is called before the first frame update

    private AudioSource source;

    public int[] needKey;
    public bool isLocked
    {
        get;
        protected set;
    } = true;

    GameObject slot;
    public string open_fail_warning = "You Don't Have Key";

    override public void Start()
    {
        base.Start();
        slot = GameObject.Find("inventory_slot");
        source = gameObject.AddComponent<AudioSource>();
        source.clip = Misc.Get().doorOpen;
    }

    override protected void activate()
    {
        if(this.canActivate())
        {
            if (playerInVolume && slot)
            {
                ArrayList current_key = new ArrayList();

                foreach (Transform item in slot.transform)
                {
                    if (item.gameObject.GetComponent<Key>())
                    {
                        current_key.Add(item.gameObject.GetComponent<Key>().keyIndex);
                    }
                }

                foreach (int index in needKey)
                {
                    foreach (Transform item in slot.transform)
                    {
                        if (item.gameObject.GetComponent<Key>())
                        {
                            if (item.gameObject.GetComponent<Key>().keyIndex == index)
                            {
                                item.gameObject.GetComponent<Key>().Consume();
                                break;
                            }
                        }
                    }
                }

                isLocked = false;
                base.OnAfterInteract();
                source.Play();
            }
        }

        base.activate();
    }

    override public bool canActivate()
    {
        if(!isLocked)
        {
            return true;
        }

        if (playerInVolume && slot)
        {
            ArrayList current_key = new ArrayList();

            foreach (Transform item in slot.transform)
            {
                if (item.gameObject.GetComponent<Key>())
                {
                    current_key.Add(item.gameObject.GetComponent<Key>().keyIndex);
                }
            }

            foreach(int index in needKey)
            {
                if(!current_key.Contains(index))
                {
                    InstructionUIManager.Get().RegisterWarning(open_fail_warning, 2.0f);
                    return false;
                }
            }

            //foreach (int index in needKey)
            //{
            //    if (!current_key.Contains(index))
            //    {
            //        return false;
            //    }
            //}

            return true;
        }
        return false;
    }

    override protected void deactivate()
    {
        base.deactivate();
        source.Play();
    }
}
