using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level_0_Tutorial_Step
{
    public const int Move = 0;
    public const int Find_Key_1 = 1;
    public const int Take_Key_1 = 2;
    public const int Open_Door_1 = 3;
    public const int DIVE = 4;
    public const int RISE = 5;
    public const int LIFT = 6;
    public const int Drop = 7;
    public const int Open_Door_2 = 8;
    public const int Crowbar = 9;
    public const int Use = 10;
    public const int Finish = 11;
}

public class InteractiveEventListener_Level0 : InteractiveEventListener
{
    bool hasJump = false;
    bool hasMove = false;

    static Dictionary<string, int> map = new Dictionary<string, int>()
    {
        {"MOVE",  Level_0_Tutorial_Step.Move},
        {"KEY0ENTER",  Level_0_Tutorial_Step.Find_Key_1},
        {"KEY0TAKE",  Level_0_Tutorial_Step.Take_Key_1},
        {"DOOR0Open",  Level_0_Tutorial_Step.Open_Door_1},
        {"DIVE",  Level_0_Tutorial_Step.DIVE},
        {"RISE",  Level_0_Tutorial_Step.RISE},
        {"LIFT",  Level_0_Tutorial_Step.LIFT},
        {"DOOR1Open",  Level_0_Tutorial_Step.Open_Door_2},
        {"CROWBAR",  Level_0_Tutorial_Step.Crowbar},
        {"USE",  Level_0_Tutorial_Step.Use},
        {"FINISH",  Level_0_Tutorial_Step.Finish},
        {"DROP",  Level_0_Tutorial_Step.Drop}
    };

    private void Update()
    {
        if(!TutorialEventListener.Get().GetHasFinished(Level_0_Tutorial_Step.Move))
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                hasMove = true;

            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                hasJump = true;
            }
            if(hasJump || hasMove)
            {
                Invoke("delayDispatchMove", 3.5f);
            }
        }

        if(!TutorialEventListener.Get().GetHasFinished(Level_0_Tutorial_Step.DIVE))
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                DispatchEvent("DIVE");
            }
        }

        if(TutorialEventListener.Get().GetHasFinished(Level_0_Tutorial_Step.DIVE))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DispatchEvent("RISE");
            }
        }
    }

    override public void DispatchEvent(string eventName)
    {
        base.DispatchEvent(eventName);
        TutorialEventListener.Get().FinishCurrentTutorial(map[eventName]);
    }

    void delayDispatchMove()
    {
        DispatchEvent("MOVE");
    }
}
