using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CheckPlayerDead : MonoBehaviour
{
    private GameObject player;
    private SwimController sc;
    private bool isDead = false;
    private GameObject canvas;
    private CanvasGroup cg;
    playtestdata Recorder;

    public AudioMixerSnapshot Quiet;

    private AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ThirdPerson_can_pick_up");
        sc = player.GetComponent<SwimController>();
        canvas = gameObject;
        cg = canvas.GetComponent<CanvasGroup>();
        if(GameObject.Find("PlaytestRecorder"))
        {
            Recorder = GameObject.Find("PlaytestRecorder").GetComponent<playtestdata>();
        }

        deathSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sc.dead && !isDead)
        {
            Invoke("dead", 2);
            isDead = true;

            if(Recorder)
            {
                Recorder.record_death();
            }
            Quiet.TransitionTo(3 / 7);
            deathSound.Play();
        }
    }

    private void dead()
    {
        cg.interactable = true;
        cg.blocksRaycasts = true;
        cg.alpha = 1f;
        Time.timeScale = 0f;
        postprocessmanager.Get().enableBlur();
    }
}
