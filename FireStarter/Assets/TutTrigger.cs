using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutTrigger : MonoBehaviour
{
    public GameObject tutCanvas;
    bool firstTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && firstTime)
        {
            TutorialEventListener.Get().FinishCurrentTutorial(Level_0_Tutorial_Step.Crowbar);
            firstTime = false;
        }
    }
}
