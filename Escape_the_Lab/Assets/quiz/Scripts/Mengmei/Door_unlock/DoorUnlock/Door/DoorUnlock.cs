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

    //Password
    public GameObject dialogBox;
    private CheckPasscode checkPasscode;

    void Start()
    {
        isOpened = false;
        anim = GetComponent<Animator>();
        anim.enabled = false;
        LoadNext = GetComponent<LoadScene>();

        dialogBox.SetActive(false);
        checkPasscode = FindObjectOfType<CheckPasscode>();
    }

    void Update()
    {
        if (collides && Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.SetActive(true);
        }

        if(collides && !isOpened && checkPasscode.CodePassed())
        {
            dialogBox.SetActive(false);
            anim.enabled = true;
            anim.SetTrigger("DoorUnlock");
            isOpened = true;
            LoadNext.LoadNextScene();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        //{
            collides = true;
        //}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        ///{
            collides = false;
            dialogBox.SetActive(false);
        //}
    }
}
