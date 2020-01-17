using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level_2_Tutorial_Step
{
    public const int Shark = 0;
    public const int Fish = 1;
    public const int Door = 2;
    public const int Crowbar = 3;
    public const int Glass = 4;
    public const int Finish = 5;
}

public class InteractiveEventListener_Level2 : InteractiveEventListener
{
    static Dictionary<string, int> map = new Dictionary<string, int>()
    {
        {"SHARK",  Level_2_Tutorial_Step.Shark},
        {"FISH",  Level_2_Tutorial_Step.Fish},
        {"DOOR",  Level_2_Tutorial_Step.Door},
        {"CROWBAR",  Level_2_Tutorial_Step.Crowbar},
        {"GLASS",  Level_2_Tutorial_Step.Glass},
        {"FINISH",  Level_2_Tutorial_Step.Finish},
    };

    private void Start()
    {
        Invoke("delayDispatchMove", 5f);
    }

    private void Update()
    {
        if (TutorialEventListener.Get().GetHasFinished(Level_2_Tutorial_Step.Finish))
        {
            Invoke("delayHideAll", 10f);
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
        DispatchEvent("SHARK");
    }
}
