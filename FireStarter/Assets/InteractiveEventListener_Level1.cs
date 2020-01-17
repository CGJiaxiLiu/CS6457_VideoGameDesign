using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level_1_Tutorial_Step
{
    public const int Insructiion = 0;
    public const int Find_Air = 1;
    public const int Dive = 2;
    public const int Use_Air = 3;
    public const int Pick = 4;
    public const int Finish = 5;
}

public class InteractiveEventListener_Level1 : InteractiveEventListener
{
    static Dictionary<string, int> map = new Dictionary<string, int>()
    {
        {"INSRUCTION",  Level_1_Tutorial_Step.Insructiion},
        {"FINDAIR",  Level_1_Tutorial_Step.Find_Air},
        {"DIVE",  Level_1_Tutorial_Step.Dive},
        {"USE",  Level_1_Tutorial_Step.Use_Air},
        {"PICK",  Level_1_Tutorial_Step.Pick},
        {"FINISH",  Level_1_Tutorial_Step.Finish},
    };

    private void Start()
    {
        Invoke("delayDispatchMove", 3f);
    }

    private void Update()
    {
        if(TutorialEventListener.Get().GetHasFinished(Level_1_Tutorial_Step.Finish))
        {
            Invoke("delayHideAll", 7f);
        }
    }

    void delayHideAll()
    {
        TutorialEventListener.Get().HideAll();
    }

    override public void DispatchEvent(string eventName)
    {
        base.DispatchEvent(eventName);
        TutorialEventListener.Get().FinishCurrentTutorial(map[eventName]);
    }

    void delayDispatchMove()
    {
        DispatchEvent("INSRUCTION");
    }
}
