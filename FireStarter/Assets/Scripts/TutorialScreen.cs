using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class TutorialScreen : MonoBehaviour
{
    private CanvasGroup[] canvasGroups;
    private int stateNum;
    private float curTime;

    // Start is called before the first frame update
    void Start()
    {
        stateNum = 0;
        curTime = 0f;
    }
    private void Awake()
    {
        canvasGroups = GetComponentsInChildren<CanvasGroup>();
        //if(canvasGroup == null)
        //{
        //    Debug.LogError("Canvas Group is not a valid group.");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (stateNum == 0)
        {
            canvasGroups[0].alpha = 1f;
            canvasGroups[1].alpha = 0f;
            canvasGroups[2].alpha = 0f;
            if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                stateNum = 1;
            }
        }
        else if (stateNum == 1)
        {
            curTime += Time.deltaTime;
            if (curTime > 2)
            {
                stateNum = 2;
            }
        }
        else if (stateNum == 2)
        {
            curTime = 0f;
            canvasGroups[0].alpha = 0f;
            canvasGroups[1].alpha = 1f;
            canvasGroups[2].alpha = 0f;
            if (Input.GetKeyUp(KeyCode.C))
            {
                stateNum = 3;
            }
        }
        else if (stateNum == 3)
        {
            curTime += Time.deltaTime;
            if (curTime > 2)
            {
                stateNum = 4;
            }
        }
        else if (stateNum == 4)
        {
            curTime = 0f;
            canvasGroups[0].alpha = 0f;
            canvasGroups[1].alpha = 0f;
            canvasGroups[2].alpha = 1f;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                stateNum = 5;
            }
        }
        else if (stateNum == 5)
        {
            curTime += Time.deltaTime;
            if (curTime > 2)
            {
                stateNum = 6;
            }
        }
        else
        {
            curTime = 0f;
            canvasGroups[0].alpha = 0f;
            canvasGroups[1].alpha = 0f;
            canvasGroups[2].alpha = 0f;
        }
    }
}
