using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diveController : MonoBehaviour
{
    private SwimController m_swimming_player;
    private float init_dist_to_player;

    public float dist_to_watersurface = 1.0f;
    public float dist_to_ceiling = 0.5f;

    GameObject water_surf;


    private void Awake()
    {
        water_surf = GameObject.FindWithTag("Water");
    }

    private void Start()
    {
        m_swimming_player = GameObject.FindGameObjectWithTag("Player").GetComponent<SwimController>();
        init_dist_to_player = m_swimming_player.transform.position.y - transform.position.y;
        
        check_onWater();
    }

    private void Update()
    {
        Vector3 pos = m_swimming_player.transform.position;

        if (m_swimming_player.isOnWater())
        {
            pos.y = water_surf.transform.position.y - dist_to_watersurface;
        }
        else pos.y = transform.position.y;

        //if (!m_swimming_player.IsSwimming()) pos.y = m_swimming_player.transform.position.y - init_dist_to_player * 10;

        gameObject.transform.position = pos;

    }

    public void rise(float RisingSpeed) {
        //make sure the water collider floor won't be too far away from the current player position
        if(transform.position.y < m_swimming_player.transform.position.y-init_dist_to_player)
            teleport_waterCollider();


        Vector3 pos = gameObject.transform.position;
        pos.y = Mathf.Min(pos.y+ RisingSpeed * Time.deltaTime, water_surf.transform.position.y - dist_to_watersurface);

        RaycastHit hit;

        //1.51 is the height of the character from its center
        Vector3 head_pos = m_swimming_player.transform.position;
        head_pos.y += 1.51f; //1.51 is the height of the character from its center
        Physics.Raycast(head_pos, transform.TransformDirection(Vector3.up), out hit, dist_to_ceiling);
        if (!Physics.Raycast(head_pos, transform.TransformDirection(Vector3.up), out hit, dist_to_ceiling)
            || hit.collider.tag != "Floor")
        {
            gameObject.transform.position = pos;
        }
        check_onWater();

    }


    public void sink(float SinkingSpeed) {

        Vector3 pos = gameObject.transform.position;

        //we assume that the y position of the player won't go deeper than -1000
        pos.y = Mathf.Max(-1000, pos.y - SinkingSpeed * Time.deltaTime);
        pos.y = Mathf.Max(m_swimming_player.transform.position.y - init_dist_to_player * 1.1f, pos.y - SinkingSpeed * Time.deltaTime);
        gameObject.transform.position = pos;
        

    }


    public bool check_onWater() {
        if (water_surf.transform.position.y - transform.position.y <= dist_to_watersurface + 0.1f)
        {
            return true;
        }
        return false;
    }

    private void teleport_waterCollider() {
        Vector3 pos= m_swimming_player.transform.position;
        pos.y -= init_dist_to_player;
        transform.position = pos; 
    }


}

