using System;
using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Fish_Swim : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator anim;
    private Vector3 wayPoint;

    private GameObject water;
    private GameObject[] walls;
    // Start is called before the first frame update
    void Start()
    {
        water = GameObject.FindGameObjectWithTag("Water");
        walls = GameObject.FindGameObjectsWithTag("Walls");

        SetDestination();

        wayPoint = new Vector3(Random.Range(-10, 10), 0 , Random.Range(-10, 10));
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance <= 0 && !navMeshAgent.pathPending)
        {
            setNextWaypoint();
        }

        anim.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);

    }

    private void setNextWaypoint()
    {
        wayPoint = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));

        SetDestination();
    }

    private void SetDestination()
    {
        try
        {
            navMeshAgent.SetDestination(wayPoint);
        }
        catch (Exception e)
        {
            print(e);
        }
    }

}
