using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    private bool canOpen;
    private bool isOpened;
    private Animator anim;
    private bool keyObtained;

    void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;
        keyObtained = GameObject.Find("Player").GetComponent<Player>().keyobtained;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) )
        {
            if (canOpen && !isOpened)
            {
                anim.SetTrigger("Opening");
                isOpened = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D" && keyObtained == true)
        {
            canOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }
}
