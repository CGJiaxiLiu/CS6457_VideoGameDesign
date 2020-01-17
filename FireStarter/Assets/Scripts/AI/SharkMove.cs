using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class SharkMove : MonoBehaviour
{
    private SharkWaypointManager m_AIManager;
    private NavMeshAgent agent;

    private bool m_hasTarget = false;
    private bool m_isTurning;

    private Vector3 m_wayPoint;
    private Vector3 m_lastWaypoint = new Vector3(0f, 0f, 0f);

    private Animator m_animator;
    private float m_speed;
    private float m_scale;

    private Collider m_collider;
    private RaycastHit m_hit;
    public float eatspeed = 5f;
    public float chasespeed = 7f;

    public AudioMixerSnapshot Shark;
    public bool inLevel3 = false;

    public enum AIState
    {
        idle,
        eat,
        chase
    }

    public AIState aiState;

    private GameObject targetFish;

    void SetUpNPC()
    {
        //float m_scale = Random.Range(0f, 0f);
        //transform.localScale += new Vector3(m_scale * 1.5f, m_scale, m_scale);
        float tempy = 0f;
        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            GameObject floor = hit.collider.gameObject;
            if (floor.tag == "Floor")
            {
                tempy = Vector3.Distance(transform.position, hit.point);
            }
        }
        */
        NavMeshHit Hit;
        if(NavMesh.SamplePosition(transform.position, out Hit, 10.0f, NavMesh.AllAreas))
        {
            tempy = Vector3.Distance(transform.position, Hit.position);
        }
        agent.baseOffset = tempy;
        agent.stoppingDistance = 0.5f;
        agent.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.AddComponent<NavMeshAgent>();
        m_AIManager = transform.parent.GetComponentInParent<SharkWaypointManager>();
        m_animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //agent.enabled = false;

        SetUpNPC();
        aiState = AIState.idle;        
    }

    bool CanFindTarget(float start = 1f, float end = 7f)
    {
        if (m_AIManager.Waypoints.Count == 0)
        {
            m_wayPoint = transform.position;
        }
        else
        {
            m_wayPoint = m_AIManager.RandomWaypoint();
        }
        if (m_wayPoint == m_lastWaypoint)
        {
            //m_wayPoint = m_AIManager.RandomWaypoint();
            return false;
        }
        else
        {
            
            m_lastWaypoint = m_wayPoint;
            m_speed = Random.Range(start, end);
            SetDest();
            return true;
        }
    }

    bool CanFindFish(float speed = 5f)
    {
        targetFish = m_AIManager.ClosestDeadFish(transform.position);
        if (targetFish == null)
        {
            m_wayPoint = transform.position;
            return false;
        }
        else
        {
            m_wayPoint = targetFish.transform.position;
        }
        m_speed = speed;
        SetDest();
        return true;
        
    }

    void SetDest()
    {
        agent.speed = m_speed;
        if (m_animator != null)
        {
            m_animator.speed = m_speed;
        }
        Vector3 flooredWP = m_wayPoint;

        // find the floor that the waypoint is in
        GameObject floor = null;
        Vector3 intersectionPt;
        /*
        RaycastHit hit;
        if (Physics.Raycast(flooredWP, Vector3.down, out hit))
        {

            floor = hit.collider.gameObject;
            if (floor.tag == "Floor")
            {
                intersectionPt = hit.point;

                flooredWP.y = intersectionPt.y;
            }
        }
        */
        NavMeshHit Hit;
        if (NavMesh.SamplePosition(flooredWP, out Hit, 10.0f, NavMesh.AllAreas))
        {
            intersectionPt = Hit.position;
            flooredWP.y = intersectionPt.y;
        }
        // flooredWP.y = 0;
        agent.SetDestination(flooredWP);
        
        
    }

    void YInterp()
    {
        float y_dist = Vector3.Distance(m_wayPoint, transform.position);
        float tempy = 0f;
        /*
        RaycastHit hit;
        if (Physics.Raycast(m_wayPoint, Vector3.down, out hit, Mathf.Infinity))
        {
            GameObject floor = hit.collider.gameObject;
            //Debug.Log(floor.tag);
            if (floor.tag == "Floor")
            {
                tempy = Vector3.Distance(m_wayPoint, hit.point);
            }
        }
        */
        NavMeshHit Hit;
        if (NavMesh.SamplePosition(m_wayPoint, out Hit, 10.0f, NavMesh.AllAreas))
        {
            tempy = Vector3.Distance(m_wayPoint, Hit.position);
        }
        //tempy = m_wayPoint.y - GameObject.FindWithTag("Floor").transform.position.y + 1.5f;

        //if (Physics.Raycast(m_wayPoint, Vector3.down, out hit,Mathf.Infinity, 12))
        //{
        //    tempy = Vector3.Distance(m_wayPoint, hit.point);
        //    Debug.DrawRay(m_wayPoint, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //}
        //else
        //{
        //    Debug.Log("ray not hit");
        //    Debug.DrawRay(m_wayPoint, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);

        //}
        //Debug.Log(tempy);

        agent.baseOffset = (Mathf.Lerp(agent.baseOffset, tempy, m_speed * Time.deltaTime / y_dist));
        
        /*float y_dist = Vector3.Distance(m_wayPoint, transform.position);
        agent.baseOffset = Mathf.Lerp(agent.baseOffset + transform.parent.position.y, m_wayPoint.y, m_speed * Time.deltaTime / y_dist) - transform.parent.position.y;*/
        float lookrotx = Quaternion.LookRotation(m_wayPoint - transform.position).eulerAngles.x;
        transform.Rotate(Mathf.Lerp(transform.rotation.x, lookrotx, agent.angularSpeed * Time.deltaTime), transform.rotation.y, transform.rotation.z, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
       //Debug.Log("on trigger script");
        if (other.gameObject.tag == "Player")
        {
            if(inLevel3)
            {
                if(!ActivateAI.Get())
                {
                    return;
                }
            }

            if (aiState != AIState.chase)
            {
                m_hasTarget = false;
            }
            aiState = AIState.chase;
            Shark.TransitionTo(6 / 7);
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (m_AIManager.DeadFish.Count != 0)
        {
            if(aiState != AIState.eat)
            {
                m_hasTarget = false;
            }
            aiState = AIState.eat;
        }
        else if (aiState != AIState.chase)
        {
            if (aiState != AIState.idle)
            {
                m_hasTarget = false;
            }
            aiState = AIState.idle;
        }

        switch (aiState)
        {
            case AIState.idle:
                Idle();
            break;
            case AIState.eat:
                Eat();
            break;
            case AIState.chase:
                Chase();
            break;
        }

        
    }
    void Idle()
    {
        if (!m_hasTarget)
        {
            m_hasTarget = CanFindTarget();
        }
        else
        {
            YInterp();
        }

        if (agent.remainingDistance - agent.stoppingDistance <= 0 && agent.pathPending == false)
        {
            m_hasTarget = false;
        }
    }
    void Eat()
    {
        if (!m_hasTarget)
        {
            m_hasTarget = CanFindFish(eatspeed);
        }
        else
        {
            YInterp();
        }

        if (agent.remainingDistance - agent.stoppingDistance <= 0 && agent.pathPending == false)
        {
            Destroy(targetFish);
            m_hasTarget = false;
        }
    }
    void Chase()
    {
        //Debug.Log("chasing");
        m_hasTarget = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_wayPoint = player.transform.position;
        m_speed = chasespeed;
        SetDest();
        YInterp();
        //Debug.Log(Mathf.Abs(transform.position.y - player.transform.position.y));
        if (agent.remainingDistance - agent.stoppingDistance <= 0.3f && agent.pathPending == false)
        {
            player.GetComponent<SwimController>().kill();
        }
    }
}
