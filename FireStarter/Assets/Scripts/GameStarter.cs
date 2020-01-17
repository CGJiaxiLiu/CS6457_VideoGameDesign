using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameStarter : MonoBehaviour
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
    public void StartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("level 0");
        //levelchanger.GetComponent<LevelChanger>().FadeOut("level 0");
        background.TransitionTo(3 / 7);
    }
}
