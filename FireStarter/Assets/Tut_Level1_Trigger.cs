using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Level1_Trigger : MonoBehaviour
{
    public GameObject tutCanvas0;
    public GameObject tutCanvas1;
    bool firstTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && firstTime)
        {
            if (GameObject.Find("back_slot").transform.Find("DivingBottle") != null)
            {
                firstTime = false;
                tutCanvas1.GetComponent<CanvasGroup>().alpha = 1.0f;
                Invoke("disableInstrcution", 3);
            }
            else
            {
                tutCanvas0.GetComponent<CanvasGroup>().alpha = 1.0f;
                Invoke("disableInstrcution", 3);
            }
        }
    }

    private void disableInstrcution()
    {
        tutCanvas0.GetComponent<CanvasGroup>().alpha = 0.0f;
        tutCanvas1.GetComponent<CanvasGroup>().alpha = 0.0f;
    }
}
