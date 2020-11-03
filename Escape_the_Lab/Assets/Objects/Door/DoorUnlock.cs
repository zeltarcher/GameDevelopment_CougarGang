using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    private bool canOpen;
    private bool isOpened;
    private Animator anim;
    private LoadScene LoadNext;
    private KeyPicker inventory;


    void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;
        LoadNext = GetComponent<LoadScene>();
        inventory = FindObjectOfType<KeyPicker>();
        anim.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) )
        {
            Debug.Log(inventory.getKeys());
            if (canOpen && !isOpened && inventory.getKeys() > 0)
            {
                Debug.Log("pressed");
                anim.enabled = true;
                anim.SetTrigger("Opening");                
                isOpened = true;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpening"))
        {
            LoadNext.LoadNextScene();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
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
