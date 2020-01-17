using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UIstate2
{
    Nothing = 0,
    Hint = 1,
    Crowbar = 2,
    Spear = 3,
    Door = 4
}

public class TutorialScreenForLevel2 : MonoBehaviour
{
    private CanvasGroup[] canvasGroups;
    private UIstate2 state = UIstate2.Nothing;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroups = GetComponentsInChildren<CanvasGroup>();
        Invoke("ShowHint", 0.5f);
        Invoke("ShowNothing", 5.5f);
        Invoke("ShowCrowbar", 6f);
        Invoke("ShowNothing", 11.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNothing()
    {
        state = UIstate2.Nothing;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
    }

    public void ShowHint()
    {
        state = UIstate2.Hint;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowCrowbar()
    {
        state = UIstate2.Crowbar;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;
    }

    public void ShowSpear()
    {
        state = UIstate2.Spear;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;

        Invoke("ShowNothing", 5f);
    }

    public void ShowDoor()
    {
        state = UIstate2.Door;
        foreach (CanvasGroup temp in canvasGroups)
        {
            temp.alpha = 0f;
        }
        canvasGroups[(int)state - 1].alpha = 1f;

        Invoke("ShowNothing", 5f);
    }
}

