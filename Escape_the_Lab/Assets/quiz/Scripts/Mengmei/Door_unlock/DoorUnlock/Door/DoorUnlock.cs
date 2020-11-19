using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorUnlock : MonoBehaviour
{
    private bool collides;
    private bool isOpened;
    private Animator anim;
    private LoadScene LoadNext;
    public bool khaBomb = false;

    //Password
    public GameObject dialogBox;
    private CheckPasscode checkPasscode;

    AudioSource _audiosrc;
    AudioClip success, failed;

    void Start()
    {
        isOpened = false;
        anim = GetComponent<Animator>();
        anim.enabled = false;
        LoadNext = GetComponent<LoadScene>();

        dialogBox.SetActive(false);
        checkPasscode = FindObjectOfType<CheckPasscode>();

        _audiosrc = GetComponent<AudioSource>();
        success = Resources.Load<AudioClip>("Win3");
        failed = Resources.Load<AudioClip>("Answer_Wrong");
    }

    void Update()
    {
        if(khaBomb == true)
        {
            bombDoor();
            khaBomb = false;
        }

        if (collides && Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.SetActive(true);
            if(checkPasscode.CodePassed())
            {
                _audiosrc.PlayOneShot(success);
            }
            else
            {
                _audiosrc.PlayOneShot(failed);
            }
        }

        if(collides && !isOpened && checkPasscode.CodePassed())
        {
            dialogBox.SetActive(false);
            anim.enabled = true;
            anim.SetTrigger("DoorUnlock");
            isOpened = true;
            GameObject go = GameObject.Find("DoorClose");
            if(go)
                Destroy(go.gameObject);
            //LoadNext.LoadNextScene();
        }
        
    }

    void bombDoor()
    {
        dialogBox.SetActive(false);
        anim.enabled = true;
        anim.SetTrigger("DoorUnlock");
        _audiosrc.PlayOneShot(success);
        isOpened = true;
        GameObject go = GameObject.Find("DoorClose");
        if (go)
            Destroy(go.gameObject);
        //LoadNext.LoadNextScene();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            collides = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            collides = false;
            dialogBox.SetActive(false);
        }
    }
}
