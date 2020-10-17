using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private AudioSource[] mySounds;

    static AudioSource SFX_audioSrc, Water_audioSrc, IngameBG_audioSrc;
    public static AudioClip Ingame_Music_Level_1, main_jumpSound, main_dieSound, main_walkSound, main_hitSound, water_raisingSound;

    AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        Ingame_Music_Level_1 = Resources.Load<AudioClip>("Ingame_Music_Level_1");
        main_jumpSound = Resources.Load<AudioClip>("Main_jump");
        main_dieSound = Resources.Load<AudioClip>("Main_Die");
        main_walkSound = Resources.Load<AudioClip>("Main_Walk");
        main_hitSound = Resources.Load<AudioClip>("Main_Hurt");



        SFX_audioSrc = GetComponent<AudioSource>();

        Water_audioSrc = GetComponent<AudioSource>();
        Water_audioSrc.clip = water_raisingSound;

        IngameBG_audioSrc = GetComponent<AudioSource>();
        IngameBG_audioSrc.PlayOneShot(Ingame_Music_Level_1);
        IngameBG_audioSrc.loop = true;

        audioMixer = Resources.Load<AudioMixer>("AudioMixer");
    }

    public static void PlayMainSound(string clip)
    {
        switch (clip)
        {
            case "walk":
                SFX_audioSrc.PlayOneShot(main_walkSound);
                break;
            case "die":
                SFX_audioSrc.PlayOneShot(main_dieSound);
                break;
            case "jump":
                SFX_audioSrc.PlayOneShot(main_jumpSound);
                break;
            case "hit":
                SFX_audioSrc.PlayOneShot(main_hitSound);
                break;
            
        }
    }

    public static void PlayWaterSound()
    {
        //Debug.Log("play water sound");
        //if (!Water_audioSrc.isPlaying)
        if(Water_audioSrc.time >= water_raisingSound.length || Water_audioSrc.time <= 0)
        {
            //Debug.Log("Get in play water sound==========================");
            Water_audioSrc.Play();
        }
    }

    /*public static void Switch_Mute_SFX()
    {

        Debug.Log("===========================================SFX SWITCH");
        if (SFX_audioSrc.mute)
        {
            Debug.Log("FALSE SFX");
            SFX_audioSrc.mute = false;
            Water_audioSrc.mute = false;
        }
        else
        {
            Debug.Log("TRUE SFX");
            SFX_audioSrc.mute = true;
            Water_audioSrc.mute = true;
        }
    }
    public static void Switch_Mute_Music()
    {

        Debug.Log("=========================================SOUND SWITCH");
        if (IngameBG_audioSrc.mute)
        {
            Debug.Log("FALSE Sound");
            IngameBG_audioSrc.mute = false;
        }
        else
        {
            Debug.Log("TRUE Sound");
            IngameBG_audioSrc.mute = true;
        }
    }*/
}
