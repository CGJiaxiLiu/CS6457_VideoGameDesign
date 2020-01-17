using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    public AudioMixerSnapshot NoShark;
    public AudioMixerSnapshot Shark;
    public float bpm = 140;

    public GameObject SharkAI;

    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;
    private SharkMove sharkMove;

    private string state;

    // Start is called before the first frame update
    void Start()
    {
        if(SharkAI)
        {
            sharkMove = SharkAI.GetComponent<SharkMove>();
        }
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote * 2;
        m_TransitionOut = m_QuarterNote * 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (sharkMove)
        {
            state = sharkMove.aiState.ToString();
            if (state == "Chase")
            {
                Shark.TransitionTo(m_TransitionIn);
            }
        }
    }

}
