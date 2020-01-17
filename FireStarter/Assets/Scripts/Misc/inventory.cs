using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemSize
{
    NULL = 0,
    SMALL = 1,
    MEDIUM = 2,
    LARGE = 3
}

public class inventory : MonoBehaviour
{
    public int small_slot_count = 6;

    public GameObject slot_prefab;
    public GameObject item_prefab;

    private ArrayList slot_array_small = new ArrayList();
    private GameObject slot_large;
    public GameObject slot_medium;

    private ArrayList item_array_small = new ArrayList();
    private Pickupable item_large = null;
    public Pickupable item_medium = null;
    private bool canPickUp = true;
    public GameObject sparkle_prefab;

    // Start is called before the first frame update
    void Start()
    {
        small_slot_count = Mathf.Min(small_slot_count, 9);

        RectTransform canvas_trans = GetComponent<RectTransform>();
        float canvasWidth = canvas_trans.rect.width;
        float canvasHeight = canvas_trans.rect.height;

        for (int i = 0; i < small_slot_count; i++)
        {
            GameObject temp = GameObject.Instantiate(slot_prefab, gameObject.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
            RectTransform trans = temp.GetComponent<RectTransform>();
            Vector3 pos = Vector3.zero;
            pos.x = canvasWidth - (small_slot_count - 1 - i) * trans.rect.width;
            pos.y = canvasHeight;
            trans.position = pos;
            slot_array_small.Add(temp);
        }

        GameObject temp_large_slot = GameObject.Instantiate(slot_prefab, gameObject.transform);
        temp_large_slot.transform.GetChild(0).GetComponent<Text>().text = "[";
        RectTransform l_trans = temp_large_slot.GetComponent<RectTransform>();
        Vector3 l_pos = Vector3.zero;
        l_pos.x = canvasWidth - l_trans.rect.width;
        l_pos.y = canvasHeight - l_trans.rect.height;
        l_trans.position = l_pos;
        slot_large = temp_large_slot;

        GameObject temp_medium_slot = GameObject.Instantiate(slot_prefab, gameObject.transform);
        temp_medium_slot.transform.GetChild(0).GetComponent<Text>().text = "]";
        RectTransform m_trans = temp_medium_slot.GetComponent<RectTransform>();
        Vector3 m_pos = Vector3.zero;
        m_pos.x = canvasWidth;
        m_pos.y = canvasHeight - m_trans.rect.height;
        m_trans.position = m_pos;
        slot_medium = temp_medium_slot;

    }

    private void Update()
    {
        if (Time.timeScale <= 0.01f) { Cursor.visible = true; }
        else { Cursor.visible = false;  }
            if (Input.GetButtonDown("Interact"))
        {
            if(item_medium)
            {
                if(!item_large)
                {
                    if (item_medium.GetComponent<Usable>())
                    {
                        Usable use = item_medium.GetComponent<Usable>();
                        item_medium.GetComponent<Usable>().Use();
                    }
                }
                else
                {
                    InstructionUIManager.Get().RegisterWarning("You need to drop first", 2.0f);
                }
            }
        }

        if(Input.GetButton("Interact"))
        {
            if (item_medium)
            {
                if (item_medium.GetComponent<Usable>())
                {
                    Usable use = item_medium.GetComponent<Usable>();
                    if(use.continuously)
                    {
                        item_medium.GetComponent<Usable>().Use();
                    }
                }
            }
        }

        if(Input.GetButtonDown("Drop"))
        {
            if (item_large)
            {
                item_large.dropItem();
                item_large = null;
                refreshUI();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("isLift", false);

                if (!item_medium)
                {
                    InstructionUIManager.Get().Deactive(UI_Type.Keyboard);
                }
            }
            else if (item_medium)
            {
                item_medium.dropItem();
                item_medium = null;
                refreshUI();
                InstructionUIManager.Get().Deactive(UI_Type.RightClick);
                InstructionUIManager.Get().Deactive(UI_Type.Keyboard);
            }
        }
    }

    public bool addItem(Pickupable obj)
    {
        if(item_large)
        {
            InstructionUIManager.Get().RegisterWarning("You need to drop first", 2.0f);
            return false;
        }

        if(!canPickUp)
        {
            return false;
        }

        if (obj.targetSlot == "front_slot")
        {
            item_large = obj;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("isLift", true);
        }

        else if (obj.targetSlot == "inventory_slot" && item_array_small.Count < small_slot_count)
        {
            item_array_small.Add(obj);
        }

        else if (obj.targetSlot == "back_slot")
        {
            if (item_medium)
            {
                item_medium.dropItem();
            }
            item_medium = obj;
        }

        canPickUp = false;
        Invoke("toggleCanPickUp", 0.5f);
        refreshUI();
        return true;
    }

    public void toggleCanPickUp()
    {
        canPickUp = !canPickUp;
    }

    public void deleteItem(Pickupable obj)
    {
        if(item_array_small.Contains(obj))
        {
            item_array_small.Remove(obj);
        }
        else if(item_large == obj)
        {
            item_large = null;
        }
        else if(item_medium == obj)
        {
            item_medium = null;
        }

        refreshUI();
    }

    public void refreshUI()
    {
        foreach(GameObject slot in slot_array_small)
        {
            foreach (Transform child in slot.transform)
            {
                if (child.name != "index")
                {
                    Destroy(child.gameObject);
                }
            }
        }


        foreach (Transform child in slot_large.transform)
        {
            if (child.name != "index")
            {
                Destroy(child.gameObject);
            }
        }

        if (item_large)
        {
            Sprite icon = item_large.item_icon;
            GameObject temp = GameObject.Instantiate(item_prefab);
            Image image_comp = temp.GetComponent<Image>();
            image_comp.sprite = icon;
            temp.transform.SetParent(slot_large.transform);
            temp.GetComponent<RectTransform>().transform.position = slot_large.transform.position;
        }

        foreach (Transform child in slot_medium.transform)
        {
            if (child.name != "index")
            {
                Destroy(child.gameObject);
            }
        }
        if (item_medium)
        {
            Sprite icon = item_medium.item_icon;
            GameObject temp = GameObject.Instantiate(item_prefab);
            Image image_comp = temp.GetComponent<Image>();
            image_comp.sprite = icon;
            temp.transform.SetParent(slot_medium.transform);
            temp.GetComponent<RectTransform>().transform.position = slot_medium.transform.position;
        }

        for (int i = 0; i < item_array_small.Count; i++)
        {
            Sprite icon = (item_array_small[i] as Pickupable).item_icon;
            GameObject temp = GameObject.Instantiate(item_prefab);
            Image image_comp = temp.GetComponent<Image>();
            image_comp.sprite = icon;
            temp.transform.SetParent((slot_array_small[i] as GameObject).transform);
            temp.GetComponent<RectTransform>().transform.position = (slot_array_small[i] as GameObject).transform.position;
        }
    }

    //public ItemSize GetCurrentItemSize()
    //{
    //    if(!currentItem)
    //    {
    //        return ItemSize.NULL;
    //    }
    //    else if(currentItem == item_large)
    //    {
    //        return ItemSize.LARGE;
    //    }
    //    else if(currentItem == item_medium)
    //    {
    //        return ItemSize.MEDIUM;
    //    }
    //    else
    //    {
    //        return ItemSize.SMALL;
    //    }
    //}

}
