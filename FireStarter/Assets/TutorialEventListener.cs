using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventListener : MonoBehaviour
{
    public static TutorialEventListener __Instance;
    public static TutorialEventListener Get()
    {
        return __Instance;
    }

    Animator[] animators;
    bool[] hasFinished;

    public bool GetHasFinished(int step)
    {
        if(step < hasFinished.Length)
        {
            return hasFinished[step];
        }
        else
        {
            return false;
        }
    }

    int currentTutorialIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        __Instance = this;
        animators = GetComponentsInChildren<Animator>();

        hasFinished = new bool[animators.Length];
        for(int i = 0; i < animators.Length; i++)
        {
            hasFinished[i] = false;
        }

        StartCurrentTutorial();
        foreach(Transform ts in transform)
        {
            ts.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }

    protected void StartCurrentTutorial()
    {
        if(currentTutorialIndex < animators.Length)
        {
            animators[currentTutorialIndex].SetTrigger("Show");
        }
    }

    public void FinishCurrentTutorial(int step)
    {

        hasFinished[step] = true;

        if (step == currentTutorialIndex && currentTutorialIndex < animators.Length)
        {
            animators[currentTutorialIndex].SetTrigger("Hide");
            currentTutorialIndex++;

            if(currentTutorialIndex < animators.Length)
            {
                while (currentTutorialIndex < animators.Length && hasFinished[currentTutorialIndex])
                {
                    currentTutorialIndex++;
                }
            }

            Invoke("StartCurrentTutorial", 0.4f);
        }
    }

    public void HideAll()
    {
        foreach(Animator i in animators)
        {
            i.SetTrigger("Hide");
        }
    }

}
