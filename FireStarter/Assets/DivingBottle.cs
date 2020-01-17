using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DivingBottle : Usable
{
    bool can_use = true;
    public float remain_air = 75.0f;
    public float use_speed = 30.0f;
    public Sprite usedIcon;
    PressureGauge gauge;
    float full_air;
    GameObject body;

    private void Start()
    {
        gauge = GetComponentInChildren<PressureGauge>();
        full_air = remain_air;
    }

    public override void Use()
    {
        base.Use();

        if (can_use)
        {
            if (remain_air > 0)
            {
                GameObject character = GameObject.FindGameObjectWithTag("Player");
                SwimController sc = character.GetComponent<SwimController>();
                float delta_air = Mathf.Min(Time.deltaTime * use_speed, remain_air);
                remain_air -= delta_air;
                sc.stamina += delta_air;
                sc.stamina = Mathf.Min(100.0f, sc.stamina);
                gauge.SetValue(1.0f - remain_air / full_air);
            }
            else
            {
                Pickupable pickComp = GetComponent<Pickupable>();
                pickComp.item_icon = usedIcon;
                GameObject.Find("InventoyUI").GetComponent<inventory>().refreshUI();
                can_use = false;
                body = transform.Find("Body").gameObject;
                Color c = body.GetComponent<Renderer>().material.color;
                c.r *= 0.25f;
                c.g *= 0.25f;
                c.b *= 0.25f;
                body.GetComponent<Renderer>().material.color = c;
            }
        }
    }

    void Dispose()
    {
        Pickupable pickComp = GetComponent<Pickupable>();
        pickComp.dropItem();
        GameObject.Find("InventoyUI").GetComponent<inventory>().deleteItem(pickComp);
    }

}
