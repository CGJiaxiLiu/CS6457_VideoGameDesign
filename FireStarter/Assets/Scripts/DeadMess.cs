using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DeadMess : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private GameObject water;
    private GameObject ceil;
    // Start is called before the first frame update
    void Start()
    {
        water = GameObject.FindGameObjectWithTag("Water");
        ceil = GameObject.FindGameObjectWithTag("Ceil");
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update()
    {
        if (water.transform.position.y >= ceil.transform.position.y)
        {
            if (canvasGroup.interactable)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
            }
            else
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
        }
    }
}