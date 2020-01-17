using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickupable
{
    public int keyIndex = 0;
    private Material mat;
    private Color emi;
    public float scale = 0.5f;

    override public void Start()
    {
        base.Start();
        mat = GetComponent<Renderer>().material;
        emi = mat.GetColor("_EmissionColor");
    }

    public override void Update()
    {
        base.Update();
        mat.SetColor("_EmissionColor", emi);

        if (transform.parent == null)
        {
            mat.SetColor("_EmissionColor", (4 + 2 * Mathf.Sin(Time.time * 3)) * emi);
        }
    }

    override protected void Pickup(bool random = false)
    {
        base.Pickup(random);
        transform.localScale = scale * transform.localScale;
    }

}
