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
            mixer.SetFloat("SoundVolume", -15f);
        else
            mixer.SetFloat("SoundVolume", -80);
    }

    public void SwitchSFXLv()
    {
        Debug.Log("SFX Volume");
        mixer.GetFloat("SFXVolume", out value);
        if (value <= -40f)
            mixer.SetFloat("SFXVolume", -10f);
        else
            mixer.SetFloat("SFXVolume", -80f);
    }

    // Start is called before the first frame update
    void Start()
    {
        mixer.SetFloat("SFXVolume", -10f);
        mixer.SetFloat("SoundVolume", -15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
