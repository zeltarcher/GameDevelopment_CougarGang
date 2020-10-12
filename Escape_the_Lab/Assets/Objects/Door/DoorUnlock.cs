﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    private bool canOpen;
    private bool isOpened;
    private Animator anim;

    private bool keyObtained;
    private LoadScene LoadNext;

    void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;
        LoadNext = GetComponent<LoadScene>();
    }

    void Update()
    {
        //keyObtained = GameObject.Find("Player").GetComponent<Player>().keyobtained;
        if (Input.GetKeyDown(KeyCode.I) )
        {
            if (canOpen && !isOpened)
            {
                anim.SetTrigger("Opening");
                isOpened = true;
                LoadNext.LoadNextScene();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D" && keyObtained == true)
        {
            canOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            canOpen = false;
        }
    }
}
