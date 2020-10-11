using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerControl : MonoBehaviour
{
    public AudioMixer mixer;
    private float value;
    public void SwitchSoundLv()
    {        
        mixer.GetFloat("SoundVolume", out value);
        Debug.Log("Sound volume");
        if (value <= -40f)
            mixer.SetFloat("SoundVolume", 0f);
        else
            mixer.SetFloat("SoundVolume", -40f);
    }

    public void SwitchSFXLv()
    {
        Debug.Log("SFX Volume");
        mixer.GetFloat("SFXVolume", out value);
        if (value <= -40f)
            mixer.SetFloat("SFXVolume", 0f);
        else
            mixer.SetFloat("SFXVolume", -40f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
