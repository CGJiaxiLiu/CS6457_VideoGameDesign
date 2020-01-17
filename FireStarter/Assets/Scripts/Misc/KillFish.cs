using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFish : MonoBehaviour
{
    public float collision_radius = 0.5f;
    public Vector3 offset = new Vector3(0, 1, 0);
    public bool showGizmos = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Spear"))
        {
            if (GameObject.Find("Spear").GetComponent<spear>().isUsing)
            {
                Vector3 center = transform.position + offset;

                Collider[] allOverlappingColliders = Physics.OverlapSphere(center, collision_radius);

                foreach (Collider c in allOverlappingColliders)
                {

                    GameObject fish_Prefab = c.gameObject;

                    if (fish_Prefab.CompareTag("Fish"))
                    {
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

                        Destroy(fish_Prefab);
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + offset, collision_radius);
        }
    }
}
