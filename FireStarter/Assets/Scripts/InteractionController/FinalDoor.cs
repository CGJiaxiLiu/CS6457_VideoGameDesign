using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class FinalDoor : Door
{
    private GameObject canvas;
    private CanvasGroup cg;
    public string canvas_name = "Level Clear Canvas";
    public bool victory = false;

    public AudioMixerSnapshot Quiet;
    private AudioSource victorySound;


    override public void Start()
    {
        base.Start();
        victorySound = this.GetComponent<AudioSource>();
    }

    override public void Update()
    {
        base.Update();
        Cheat();
    }

    override protected void activate()
    {
        base.activate();

        if(canActivate())
        {
            victory = true;
            canvas = GameObject.Find(canvas_name);
            cg = canvas.GetComponent<CanvasGroup>();
            Invoke("levelClear", 2);
        }
    }

    public void levelClear()
    {
        cg.interactable = true;
        cg.blocksRaycasts = true;
        cg.alpha = 1f;
        Time.timeScale = 0f;
        Quiet.TransitionTo(3 / 7);
        victorySound.Play();

        if (GameObject.Find("PlaytestRecorder"))
        {
            GameObject.Find("PlaytestRecorder").GetComponent<playtestdata>().record_pass_time();
            if (canvas_name == "Victory")
            {
                GameObject.Find("PlaytestRecorder").GetComponent<playtestdata>().report_results();
            }
        }
    }

    public void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            canvas = GameObject.Find(canvas_name);
            cg = canvas.GetComponent<CanvasGroup>();
            Invoke("levelClear", 2);
            levelClear();
        }
    }
}
