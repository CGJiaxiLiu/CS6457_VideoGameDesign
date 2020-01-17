using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    private AISpawner m_AIManager;
    public NavMeshAgent agent;

    private bool m_hasTarget = false;
    private bool m_isTurning;

    private Vector3 m_wayPoint;
    private Vector3 m_lastWaypoint = new Vector3(0f, 0f, 0f);

    private Animator m_animator;
    private float m_speed;
    private float m_scale;

    private Collider m_collider;
    private RaycastHit m_hit;

    void SetUpNPC()
    {
        //float m_scale = Random.Range(0f, 0f);
        //transform.localScale += new Vector3(m_scale * 1.5f, m_scale, m_scale);
        agent.stoppingDistance = 0.5f;
        this.tag = "Fish";
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.AddComponent<NavMeshAgent>();
        m_AIManager = transform.parent.GetComponentInParent<AISpawner>();
        m_animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        SetUpNPC();
    }

    bool CanFindTarget(float start = 1f, float end = 4f)
    {
        if (m_AIManager.Waypoints.Count == 0)
        {
            m_wayPoint = transform.position;
        }
        else
        {
            m_wayPoint = m_AIManager.RandomWaypoint();
        }
        if(m_wayPoint == m_lastWaypoint)
        {
            //m_wayPoint = m_AIManager.RandomWaypoint();
            return false;
        }
        else
        {
            //Debug.Log("set path");
            m_lastWaypoint = m_wayPoint;
            m_speed = Random.Range(start, end);
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
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_hasTarget)
        {
            m_hasTarget = CanFindTarget();
        }
        else
        {
            //float y_dist = Vector3.Distance(m_wayPoint, transform.position);
            //agent.baseOffset = Mathf.Lerp(agent.baseOffset + transform.parent.position.y, m_wayPoint.y, m_speed*Time.deltaTime/y_dist) - transform.parent.position.y;
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
            agent.baseOffset = Mathf.Lerp(agent.baseOffset, tempy, m_speed * Time.deltaTime / y_dist);
            float lookrotx = Quaternion.LookRotation(m_wayPoint - transform.position).eulerAngles.x;
            transform.Rotate(Mathf.Lerp(transform.rotation.x, lookrotx, agent.angularSpeed * Time.deltaTime),transform.rotation.y,transform.rotation.z,Space.Self);
        }

        if (agent.remainingDistance-agent.stoppingDistance <= 0 && agent.pathPending == false)
        {
            m_hasTarget = false;
        }
    }
}
