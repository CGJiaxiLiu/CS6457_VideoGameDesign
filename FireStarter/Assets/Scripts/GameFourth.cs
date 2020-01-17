using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameFourth : MonoBehaviour
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
    public void StartFourth()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_3");
        //levelchanger.GetComponent<LevelChanger>().FadeOut("Level_3");
        background.TransitionTo(3 / 7);
    }
}
