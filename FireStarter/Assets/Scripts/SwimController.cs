using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimController : MonoBehaviour
{
    private diveController m_character_level;
    private Animator m_anim;
    GameObject water_surf;

    public float stamina;

    public float rising_speed = 1.0f;
    public float sinking_speed = 1.0f;
    public float stamina_decrese_speed = 1.0f;

    public float relative_waterLevel_swim = 0.7f;

    private bool onWater = false;
    public bool dead
    {
        get;
        protected set;
    } = false;

    private bool isSwimming = false;
    private Transform headTrans;

    private AudioSource diveSound;
    private AudioSource outWaterSound;

    // Start is called before the first frame update
    void Start()
    {
        m_character_level = this.transform.parent.GetComponentInChildren<diveController>();
        m_anim = GetComponent<Animator>();
        water_surf = GameObject.Find("Water");

        onWater = m_character_level.check_onWater();
        stamina = 100;

        headTrans = GameObject.Find("EthanRightUpperLip").transform;

        diveSound = gameObject.AddComponent<AudioSource>();
        outWaterSound = gameObject.AddComponent<AudioSource>();

        diveSound.clip = Misc.Get().dive;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(stamina);
        if(dead){
            m_character_level.sink(sinking_speed/10);
            return;
        }

        //Debug.Log(water_surf.transform.position.y - transform.position.y);
        if (water_surf.transform.position.y - transform.position.y > relative_waterLevel_swim)
        {
            if (!isSwimming) diveSound.Play();
            m_anim.SetBool("IsSwimming", true);
            isSwimming = true;
        }
        else
        {
            //if (isSwimming) diveSound.Play();
            m_anim.SetBool("IsSwimming", false);
            isSwimming = false;
        }

        if (isSwimming) {
            if (Input.GetKey(KeyCode.Space))
            {
                m_character_level.rise(rising_speed);
                bool newOnWater = m_character_level.check_onWater();
                if (onWater != newOnWater)
                {
                    diveSound.Play();
                    onWater = newOnWater;
                }
            }

            if (Input.GetKey(KeyCode.C))
            {
                onWater = false;
                m_character_level.sink(sinking_speed);

            }

            bool headIsUnderwater = headTrans.position.y < water_surf.transform.position.y;

            if (!onWater && headIsUnderwater) {
                stamina = Mathf.Max(stamina - 5 * stamina_decrese_speed * Time.deltaTime, 0);
                if (stamina <= 0) {
                    dead = true;
                    m_anim.SetTrigger("Death");
                }
            }
            else {
                stamina = Mathf.Min(stamina + 1, 100);
            }
        }
    }

    public bool isOnWater() { 
        return onWater;
    }

    public bool IsSwimming() {
        return isSwimming;
    }

    public void kill() {

        if(!dead)m_anim.SetTrigger("Death");
        dead = true;
    }
}
