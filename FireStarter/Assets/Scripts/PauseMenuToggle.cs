using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    private GameObject player;
    private SwimController sc;

    private GameObject door;
    private bool victory;

    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ThirdPerson_can_pick_up");
        door = GameObject.Find("FinalDoor");
        sc = player.GetComponent<SwimController>();
        Cursor.visible = true;

    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update()
    {
        victory = door.GetComponent<FinalDoor>().victory;
        if (!(sc.dead || victory))
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (canvasGroup.interactable)
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.alpha = 0f;
                    Time.timeScale = 1f;
                    postprocessmanager.Get().disableBlur();
                }
                else
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.alpha = 1f;
                    Time.timeScale = 0f;
                    postprocessmanager.Get().enableBlur();
                }
            }
        }
    }
}
