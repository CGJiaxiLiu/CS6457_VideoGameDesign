using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscSoundPlayer : MonoBehaviour
{
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = Misc.Get().swim;
        source.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwimSound()
    {
        source.Play();
        print("SOUND");
    }
}
