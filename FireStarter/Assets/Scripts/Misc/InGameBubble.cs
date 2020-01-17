using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
public class InGameBubble : Interactable
{
    public GameObject UI_Prefab;
    protected GameObject UI_Object;
    protected Text textComp;
    protected Image imageComp;
    protected Canvas sceneCanvas;
    public Vector3 relativePosition = new Vector3();

    public bool UI_Enabled = true;
    public string UI_Content;
    //public Color BackgroundColor = new Color(0.5f, 0.5f, 0.85f, 0.5f);
    public int fontSize = 24;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        sceneCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        UI_Object = Instantiate(UI_Prefab, sceneCanvas.transform);
        textComp = UI_Object.transform.GetChild(0).gameObject.GetComponent<Text>();
        textComp.text = UI_Content;
        textComp.fontSize = fontSize;
        imageComp = UI_Object.GetComponent<Image>();
    }

    // Update is called once per frame
    override public void Update()
    {  
        if(playerInVolume && UI_Enabled)
        {
            imageComp.enabled = true;
            textComp.enabled = true;
            Vector3 ui_pos = Camera.main.WorldToScreenPoint(this.transform.position + relativePosition);
            UI_Object.transform.position = ui_pos;
            //print("UI Enabled");
        }
        else
        {
            imageComp.enabled = false;
            textComp.enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position + relativePosition, 0.1f);
    }

    void OnDestroy()
    {
        if(UI_Object)
        {
            Destroy(UI_Object);
        }
    }

    override protected void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player") && canInteract)
        {
            //print(gameObject.name);

            playerInVolume = true;
            player = c.gameObject;
        }
    }

    override protected void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player") && canInteract)
        {
            playerInVolume = false;
            player = null;
        }
    }
}
