using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UI_Type
{
    LeftClick = 0,
    RightClick = 1,
    Keyboard = 2
}

public class InstructionUIManager : MonoBehaviour
{
    private static InstructionUIManager _INSTANCE;

    public Image leftClick;
    public Image rightClick;
    public Image keyboardButton;
    public Image warning;
    private bool leftIsActive = false;
    private bool rightIsActive = false;
    private bool keyboardIsActive = false;
    private Vector3 anchor0;
    private Vector3 anchor1;
    private Vector3 anchor2;
    private float endTime = 0.0f;

    public static InstructionUIManager Get()
    {
        return _INSTANCE;
    }

    private void Update()
    {
        if (Time.time > endTime)
        {
            warning.GetComponent<CanvasGroup>().alpha = 0.0f;
        }

        if (rightIsActive)
        {
            rightClick.GetComponent<CanvasGroup>().alpha = 1.0f;
        }
        else
        {
            rightClick.GetComponent<CanvasGroup>().alpha = 0.1f;
            rightClick.transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        }

        if (leftIsActive)
        {
            leftClick.GetComponent<CanvasGroup>().alpha = 1.0f;
        }
        else
        {
            leftClick.GetComponent<CanvasGroup>().alpha = 0.1f;
            leftClick.transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        }

        if (keyboardIsActive)
        {
            keyboardButton.GetComponent<CanvasGroup>().alpha = 1.0f;
        }
        else
        {
            keyboardButton.GetComponent<CanvasGroup>().alpha = 0.1f;
            keyboardButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        }

        if (!leftIsActive && !rightIsActive && !keyboardIsActive)
        {
            SetHiddenAll();
        }

        //else if(!leftIsActive && rightIsActive)
        //{
        //    rightClick.GetComponent<RectTransform>().position = anchor0;
        //    rightClick.GetComponent<CanvasGroup>().alpha = 1.0f;
        //    leftClick.GetComponent<CanvasGroup>().alpha = 0.1f;
        //    leftClick.GetComponent<RectTransform>().position = anchor1;
        //    leftClick.transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        //}
        //else
        //{
        //    leftClick.GetComponent<CanvasGroup>().alpha = 1.0f;
        //    leftClick.GetComponent<RectTransform>().position = anchor0;

        //    rightClick.GetComponent<RectTransform>().position = anchor1;
        //    rightClick.GetComponent<CanvasGroup>().alpha = 1.0f;

        //    if(!rightIsActive)
        //    {
        //        rightClick.transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        //        rightClick.GetComponent<CanvasGroup>().alpha = 0.1f;
        //    }
        //}
    }

    private void Awake()
    {
        if (_INSTANCE != null && _INSTANCE != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _INSTANCE = this;
        }

        anchor0 = new Vector3(Screen.width * (0.5f +  1.0f / 15.0f), Screen.height / 4.0f, 0);
        anchor1 = anchor0 + new Vector3(-25, 25, 0);
        anchor2 = anchor0 + new Vector3(-25, -25, 0);
    }

    private void Start()
    {
        SetHiddenAll();
    }

    public void SetHiddenAll()
    {
        leftClick.GetComponent<CanvasGroup>().alpha = 0.0f;
        rightClick.GetComponent<CanvasGroup>().alpha = 0.0f;
        keyboardButton.GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    public void SetActive(UI_Type type, string content)
    {
        switch (type)
        {
            case UI_Type.LeftClick:
                leftClick.transform.Find("Text").gameObject.GetComponent<Text>().text = content;
                leftIsActive = true;
                break;
            case UI_Type.RightClick:
                rightClick.transform.Find("Text").gameObject.GetComponent<Text>().text = content;
                rightIsActive = true;
                break;
            case UI_Type.Keyboard:
                keyboardButton.transform.Find("Text").gameObject.GetComponent<Text>().text = content;
                keyboardIsActive = true;
                break;
        }
    }

    public void Deactive(UI_Type type)
    {
        switch(type)
        {
            case UI_Type.LeftClick:
                leftIsActive = false;
                break;
            case UI_Type.RightClick:
                rightIsActive = false;
                break;
            case UI_Type.Keyboard:
                keyboardIsActive = false;
                break;
        }
    }

    public void RegisterWarning(string content, float time)
    {
        warning.GetComponent<CanvasGroup>().alpha = 1.0f;
        warning.GetComponentInChildren<Text>().text = content;
        endTime = Time.time + time;
    }

}
