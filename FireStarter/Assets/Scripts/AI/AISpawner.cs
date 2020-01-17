using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AIObjects
{
    public string AIGroupName {get { return m_AIGroupName; } }
    public GameObject objectPrefab { get { return m_prefab; } }
    public int maxAI { get { return m_maxAI; } }
    public int spawnRate { get { return m_spawnRate; } }
    public int spawnAmount { get { return m_spawnAmount; } }
    public bool randomizeStats { get { return m_randomizeStats; } }
    public bool enableSpawner { get { return m_enableSpawner; } }

    [Header("AI Group Stats")]
    [SerializeField]
    private string m_AIGroupName;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    [Range (0f, 30f)]
    private int m_maxAI;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_spawnRate;
    [SerializeField]
    [Range(0f, 10f)]
    private int m_spawnAmount;

    [Header("Main Settings")]
    [SerializeField]
    private bool m_enableSpawner;
    [SerializeField]
    private bool m_randomizeStats;

    public AIObjects(string Name, GameObject Prefab, int MaxAI, int SpawnRate, int SpawnAmount, bool RandomizeStats)
    {
        this.m_AIGroupName = Name;
        this.m_prefab = Prefab;
        this.m_maxAI = MaxAI;
        this.m_spawnRate = SpawnRate;
        this.m_spawnAmount = SpawnAmount;
        this.m_randomizeStats = RandomizeStats;
    }
    public void SetValues(int MaxAI, int SpawnRate, int SpawnAmount)
    {
        this.m_maxAI = MaxAI;
        this.m_spawnRate = SpawnRate;
        this.m_spawnAmount = SpawnAmount;
    }
}
public class AISpawner : MonoBehaviour
{
    public List<Transform> Waypoints = new List<Transform>();

    public float spawnTimer { get { return m_SpawnTimer; } }
    public Vector3 spawnArea { get { return m_SpawnArea; } }

    [Header("Global Stats")]
    [Range(0f, 600f)]
    [SerializeField]
    private float m_SpawnTimer;
    [SerializeField]
    private Color m_SpawnColor = new Color(1f, 0f, 0f, 0.3f);
    [SerializeField]
    private Vector3 m_SpawnArea = new Vector3(20f, 10f, 20f);

    [Header("AI Group Settings")]
    public AIObjects[] AIObject = new AIObjects[5];
    
    //get waypoints
    void GetWaypoints()
    {
        Waypoints.Clear();
        Transform [] wplist = transform.GetComponentsInChildren<Transform>();
        foreach (Transform trans in wplist)
        {
            if (trans.tag == "waypoint")
            {
                Waypoints.Add(trans);
            }
        }
    }

    void RandomizeGroups()
    {
        for(int i  = 0; i < AIObject.Length; i++)
        {
            if (AIObject[i].randomizeStats)
            {
                //AIObject[i] = new AIObjects(AIObject[i].AIGroupName, AIObject[i].objectPrefab, Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10), AIObject[i].randomizeStats);
                AIObject[i].SetValues(Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10));
            }
        }
    }

    void CreateAIGroups()
    {
        foreach(AIObjects AIO in AIObject)
        {
            GameObject AIGroupSpawn = new GameObject(AIO.AIGroupName);
            AIGroupSpawn.transform.parent = this.gameObject.transform;
        }
    }

    void SpawnNPC()
    {
        foreach(AIObjects AIO in AIObject)
        {
            if (AIO.enableSpawner && AIO.objectPrefab != null)
            {
                GameObject tempGroup = GameObject.Find(AIO.AIGroupName);
                if(tempGroup.GetComponentInChildren<Transform>().childCount < AIO.maxAI)
                {
                    for (int y = 0; y < AIO.spawnAmount; y++)
                    {
                        Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);
                        GameObject tempSpawn = Instantiate(AIO.objectPrefab, RandomPosition(), randomRotation);
                        tempSpawn.transform.parent = tempGroup.transform;

                        float tempy = 0f;//= tempSpawn.transform.position.y;
                        NavMeshHit Hit;
                        if (NavMesh.SamplePosition(tempSpawn.transform.position, out Hit, 10.0f, NavMesh.AllAreas))
                        {
                            tempy = Vector3.Distance(tempSpawn.transform.position, Hit.position);
                        }
                        tempSpawn.AddComponent<UnityEngine.AI.NavMeshAgent>();
                        tempSpawn.GetComponent<UnityEngine.AI.NavMeshAgent>().baseOffset = tempy;
                        tempSpawn.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                        tempSpawn.AddComponent<AIMove>();
                    }
                }
            }
        }
    }

    public Vector3 RandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            Random.Range(-spawnArea.z, spawnArea.z)
            );
        randomPosition = transform.TransformPoint(randomPosition * 0.5f);
        return randomPosition;
    }

    public Vector3 RandomWaypoint()
    {
        int randomWP = Random.Range(0, (Waypoints.Count));
        Vector3 randomWaypoint = Waypoints[randomWP].transform.position;
        return randomWaypoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_SpawnColor;
        Gizmos.DrawCube(transform.position, spawnArea);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        RandomizeGroups();
        CreateAIGroups();
        SpawnNPC();
        //InvokeRepeating("SpawnNPC",0.5f,spawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        GetWaypoints();
    }
}
