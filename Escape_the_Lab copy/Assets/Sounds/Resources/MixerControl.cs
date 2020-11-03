using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Button btnSound;
    public Button btnSFX;
    public Sprite SFX_on;
    public Sprite SFX_off;
    public Sprite Sound_on;
    public Sprite Sound_off;
    private float value;
    public void SwitchSoundLv()
    {        
        mixer.GetFloat("SoundVolume", out value);
        //Debug.Log("Sound volume");
        if (value <= -80f)
        {
            mixer.SetFloat("SoundVolume", -15f);
            btnSound.image.sprite = Sound_on;
            //btnSound.GetComponent<Sprite>() = SFX_on;
        }
        else
        {
            mixer.SetFloat("SoundVolume", -80f);
            btnSound.image.sprite = Sound_off;
        }
    }

    public void SwitchSFXLv()
    {
        //Debug.Log("SFX Volume");
        mixer.GetFloat("SFXVolume", out value);
        if (value <= -80f)
        {
            mixer.SetFloat("SFXVolume", -15f);
            btnSFX.image.sprite = SFX_on;
        }
        else
        {
            mixer.SetFloat("SFXVolume", -80f);
            btnSFX.image.sprite = SFX_off;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mixer.SetFloat("SFXVolume", -10f);
        mixer.SetFloat("SoundVolume", -15f);

        btnSFX.image.sprite = SFX_on;
        btnSound.image.sprite = Sound_on;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
