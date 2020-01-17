using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameThird : MonoBehaviour
{
    //public GameObject levelchanger;

    public AudioMixerSnapshot background;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartThird()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
        //levelchanger.GetComponent<LevelChanger>().FadeOut("Level 2");
        background.TransitionTo(3 / 7);
    }
}
