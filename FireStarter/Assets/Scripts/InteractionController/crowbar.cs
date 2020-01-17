using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowbar : Usable
{
    private Transform old_parent;
    private Transform handle;
    Transform left_hand;
    Transform right_hand;
    private float distance;
    

    private void Start()
    {
        handle = gameObject.transform.Find("HANDLE");
        distance = (transform.position - handle.position).magnitude;
        left_hand = GameObject.Find("Left_hand_slot").transform;
        right_hand = GameObject.Find("Right_hand_slot").transform;
    }

    private void Update()
    {
        if (isUsing)
        {
            UpdateTransformation();
        }
    }

    public override void Use()
    {
        if(!isUsing)
        {
            old_parent = transform.parent;
            gameObject.transform.parent = left_hand;
            UpdateTransformation();

            isUsing = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Crowbar");
        }
        base.Use();
    }

    public void resume()
    {
        if (isUsing)
        {
            gameObject.transform.parent = old_parent;
            gameObject.transform.position = old_parent.transform.position;
            gameObject.transform.rotation = old_parent.transform.rotation;
            isUsing = false;
        }
    }

    protected void UpdateTransformation()
    {
        gameObject.transform.position = left_hand.position + (right_hand.position - left_hand.position).normalized * distance;
        gameObject.transform.rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), (right_hand.position - left_hand.position).normalized);
    }
}
