using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Door))]
public class TutorialBubble : InGameBubble
{
    public string secondText;
    override public void Update()
    {
        if(GetComponent<Door>().canActivate())
        {
            textComp.text = secondText;
        }

        base.Update();
    }
}
