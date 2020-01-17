using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UIstate
{
    nothing = 0,
    move = 1,
    key = 2,
    door = 3,
    dive = 4,
    rise = 5,
    stamaina = 6,
    block = 7,
    crowbar = 8
}

public class TutorialScreenForLevel0 : MonoBehaviour
{
    private CanvasGroup[] canvasGroups;
    private UIstate state;
    public GameObject firstDoor;
    public GameObject firstKey;
    private Door doorComp;
    private Key ketComp;
    private bool doorIsClosed = true;
    private bool lastFrameDoorIsClosed = true;
    private bool keyOnFloor = true;
    private bool lastFramekeyOnFloor = true;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroups = GetComponentsInChildren<CanvasGroup>();
        Invoke("ShowMove", 0.5f);
        doorComp = firstDoor.GetComponent<Door>();
        ketComp = firstKey.GetComponent<Key>();
    }

    // Update is called once per frame
    void Update()
    {
        doorIsClosed = doorComp.isLocked;

        if(keyOnFloor)
        {
            keyOnFloor = firstKey.transform.parent == null;
            if (!keyOnFloor && lastFramekeyOnFloor)
            {
                //Invoke("ShowDoor", 1);
                TutorialEventListener.Get().FinishCurrentTutorial(Level_0_Tutorial_Step.Open_Door_1);
            }
        }

        if (!doorIsClosed && lastFrameDoorIsClosed)
        {
            Invoke("ShowDive", 0.25f);
        }

        switch (state)
        {
            case UIstate.move:
                {
                    if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                    {
                        //Invoke("ShowNothing", 0.1f);
                        //Invoke("ShowKey", 0.5f);
                        TutorialEventListener.Get().FinishCurrentTutorial(Level_0_Tutorial_Step.Move);
                    }
                    break;
                }

            case UIstate.dive:
                {
                    if (Input.GetKeyUp(KeyCode.C))
                    {
                        Invoke("ShowNothing", 0.5f);
                        Invoke("ShowRise", 1f);
                    }
                    break;
                }

            case UIstate.rise:
                {
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        Invoke("ShowNothing", 0.5f);
                        Invoke("ShowStamina", 1f);
                        Invoke("ShowBlock", 2f);
                        Invoke("ShowNothing", 5.5f);
                    }
                    break;
                }

            default: break;
        }

        lastFrameDoorIsClosed = doorIsClosed;
        lastFramekeyOnFloor = keyOnFloor;
    }

    public void ShowNothing()
    {
        state = UIstate.nothing;
        foreach(CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
    }

    public void ShowMove()
    {
        state = UIstate.move;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowKey()
    {
        state = UIstate.key;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowDoor()
    {
        state = UIstate.door;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowDive()
    {
        state = UIstate.dive;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowRise()
    {
        state = UIstate.rise;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowStamina()
    {
        state = UIstate.stamaina;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowBlock()
    {
        state = UIstate.block;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowCrowbar()
    {
        state = UIstate.crowbar;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;

        Invoke("ShowNothing", 2.0f);
    }

}
