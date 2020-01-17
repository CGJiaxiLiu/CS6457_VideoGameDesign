using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear : Usable
{
    private Transform old_parent;
    private Transform handle;
    Transform left_hand;
    Transform right_hand;
    private float distance;
    GameObject SpearTip;
    public bool collisionEnable = false;
    public bool debug_enable = false;
    public bool GenerateKillEvent = false;
    public string KillEventName;

    private void Start()
    {
        handle = gameObject.transform.Find("HANDLE");
        distance = (transform.position - handle.position).magnitude;
        left_hand = GameObject.Find("Left_hand_slot").transform;
        right_hand = GameObject.Find("Right_hand_slot").transform;
        SpearTip = gameObject.transform.Find("SpearTip").gameObject;
    }

    private void Update()
    {
        if (isUsing)
        {
            UpdateTransformation();

            if(collisionEnable && debug_enable)
            {
                Collider[] allOverlappingColliders = Physics.OverlapSphere(SpearTip.transform.position, SpearTip.GetComponent<SphereCollider>().radius);

                foreach (Collider c in allOverlappingColliders)
                {

                    GameObject fish_Prefab = c.gameObject;

                    if (fish_Prefab.CompareTag("Fish"))
                    {
                        if(GenerateKillEvent)
                        {
                            InteractiveEventListener.Get().DispatchEvent(KillEventName);
                        }

                        print("find a fish:" + fish_Prefab.name);
                        Transform fish_trans = c.transform;
                        GameObject fish_mesh = c.gameObject;
                        GameObject dead_fish = Instantiate(fish_Prefab, fish_trans.position, fish_trans.rotation);
                        dead_fish.AddComponent<Rigidbody>();
                        dead_fish.GetComponent<Rigidbody>().maxDepenetrationVelocity = 1;
                        Destroy(dead_fish.GetComponent<AIMove>());
                        Destroy(dead_fish.GetComponent<Animator>());
                        Destroy(dead_fish.GetComponent<UnityEngine.AI.NavMeshAgent>());
                        dead_fish.transform.localScale = fish_trans.lossyScale;
                        dead_fish.name = "A Dead Fish";
                        dead_fish.transform.GetChild(0).gameObject.layer = 0;
                        dead_fish.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetFloat("_Speed", 0);
                        dead_fish.tag = "Dead Fish";
                        //dead_fish.AddComponent<Floatable>();
                        Destroy(fish_Prefab);
                    }
                }
            }

        }
    }

    public override void Use()
    {
        if (!isUsing)
        {
            old_parent = transform.parent;
            gameObject.transform.parent = left_hand;
            UpdateTransformation();

            isUsing = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Spear");
        }
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
        gameObject.transform.position = right_hand.position + (left_hand.position - right_hand.position).normalized * distance;
        gameObject.transform.rotation = Quaternion.FromToRotation(new Vector3(0, 0, 1), (left_hand.position - right_hand.position).normalized);
    }
}
