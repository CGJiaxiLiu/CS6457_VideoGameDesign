using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionOnStartingGame : MonoBehaviour
{
    private CanvasGroup canvasGroups;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroups = GetComponent<CanvasGroup>();
        Invoke("disableInstrcution", 3);
    }

    private void disableInstrcution()
    {
        canvasGroups.GetComponent<CanvasGroup>().alpha = 0.0f;
    }

}
