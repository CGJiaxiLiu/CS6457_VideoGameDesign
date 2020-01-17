using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina_bar : MonoBehaviour
{
    private SwimController m_swimming_player;
    private Slider m_slider;
    // Start is called before the first frame update
    void Start()
    {
        m_swimming_player = GameObject.FindGameObjectWithTag("Player").GetComponent<SwimController>();
        m_slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        m_slider.value = m_swimming_player.stamina;
    }
}
