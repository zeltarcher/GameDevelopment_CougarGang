using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeBGMusic : MonoBehaviour
{
    private AudioSource aSource;
    private AudioClip bgMusicLv2;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GameObject.Find("BGMusic").GetComponent<AudioSource>(); 
        bgMusicLv2 = Resources.Load<AudioClip>("Ingame_Music_Level_2");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            aSource.PlayOneShot(bgMusicLv2);
            aSource.loop = true;
        }
    }
}
