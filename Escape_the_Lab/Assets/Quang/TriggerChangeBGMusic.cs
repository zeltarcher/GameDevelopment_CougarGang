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
        bgMusicLv2 = Resources.Load<AudioClip>("Fire_Burning");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
